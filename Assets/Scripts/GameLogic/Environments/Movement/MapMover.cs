using System;
using GameLogic.Environments.Directions;
using UnityEngine;

namespace GameLogic.Environments.Movement
{
	public class MapMover : MonoBehaviour
	{
		[SerializeField] private Transform _player;
		[SerializeField] private float _moveDistance = 600f;
		[SerializeField] private float _tileSize = 200f;
		[SerializeField] private Transform[] _tiles;

		private event Action<Direction> OnPlayerMovedDistance;

		private Transform[,] _tileGrid;
		private Vector3 _lastPlayerPosition;

		private void Awake()
		{
			int gridSize = Mathf.RoundToInt(Mathf.Sqrt(_tiles.Length));
			_tileGrid = new Transform[gridSize, gridSize];

			for (int i = 0; i < _tiles.Length; i++)
			{
				int x = i / gridSize;
				int y = i % gridSize;
				_tileGrid[x, y] = _tiles[i];
			}

			_lastPlayerPosition = _player.position;
		}

		private void OnEnable() =>
			OnPlayerMovedDistance += ShiftTiles;

		private void OnDisable() =>
			OnPlayerMovedDistance -= ShiftTiles;

		private void Update()
		{
			Vector3 moveDirection = _player.position - _lastPlayerPosition;

			if (moveDirection.magnitude < _tileSize)
				return;

			if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.z))
			{
				OnPlayerMovedDistance?.Invoke(moveDirection.x > 0 ? Direction.Right : Direction.Left);
				_lastPlayerPosition = new Vector3(Mathf.Round(_player.position.x / _tileSize) * _tileSize, _lastPlayerPosition.y, _lastPlayerPosition.z);
			}
			else
			{
				OnPlayerMovedDistance?.Invoke(moveDirection.z > 0 ? Direction.Up : Direction.Down);
				_lastPlayerPosition = new Vector3(_lastPlayerPosition.x, _lastPlayerPosition.y, Mathf.Round(_player.position.z / _tileSize) * _tileSize);
			}
			Debug.Log(_lastPlayerPosition);
		}

		private void ShiftTiles(Direction direction)
		{
			switch (direction)
			{
				case Direction.Right:
					ShiftColumn(true);
					break;

				case Direction.Left:
					ShiftColumn(false);
					break;

				case Direction.Up:
					ShiftRow(true);
					break;

				case Direction.Down:
					ShiftRow(false);
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
			}
		}

		private void ShiftColumn(bool isRight)
		{
			int gridSize = _tileGrid.GetLength(0);

			if (isRight)
			{
				for (int y = 0; y < gridSize; y++)
				{
					Transform temp = _tileGrid[gridSize - 1, y];

					for (int x = gridSize - 1; x > 0; x--)
						_tileGrid[x, y] = _tileGrid[x - 1, y];

					_tileGrid[0, y] = temp;
					_tileGrid[0, y].position += Vector3.right * _moveDistance;
				}
			}
			else
			{
				for (int y = 0; y < gridSize; y++)
				{
					Transform temp = _tileGrid[0, y];

					for (int x = 0; x < gridSize - 1; x++)
						_tileGrid[x, y] = _tileGrid[x + 1, y];

					_tileGrid[gridSize - 1, y] = temp;
					_tileGrid[gridSize - 1, y].position += Vector3.left * _moveDistance;
				}
			}
		}

		private void ShiftRow(bool isUp)
		{
			int gridSize = _tileGrid.GetLength(0);

			if (isUp)
			{
				for (int x = 0; x < gridSize; x++)
				{
					Transform temp = _tileGrid[x, gridSize - 1];

					for (int y = gridSize - 1; y > 0; y--)
						_tileGrid[x, y] = _tileGrid[x, y - 1];

					_tileGrid[x, 0] = temp;
					_tileGrid[x, 0].position += Vector3.forward * _moveDistance;
				}
			}
			else
			{
				for (int x = 0; x < gridSize; x++)
				{
					Transform temp = _tileGrid[x, 0];

					for (int y = 0; y < gridSize - 1; y++)
						_tileGrid[x, y] = _tileGrid[x, y + 1];

					_tileGrid[x, gridSize - 1] = temp;
					_tileGrid[x, gridSize - 1].position += Vector3.back * _moveDistance;
				}
			}
		}
	}
}