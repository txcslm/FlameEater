using System;
using System.Collections.Generic;
using CodeBase.Const.Enums;
using CodeBase.Const.Enums.Directions;
using CodeBase.Infrastructure.Factories.Interfaces;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.GameLogic.Environments.Movement
{
	public class MapMover : MonoBehaviour
	{
		[SerializeField] private float _moveDistance = 600f;
		[SerializeField] private float _tileSize = 200f;
		[SerializeField] private Transform[] _tiles;

		private event Action<Direction> PlayerMovedDistance;

		private Transform _characterTransform;
		private Transform[,] _tileGrid;
		private Vector3 _lastCharacterPosition;
		private IGameFactory _gameFactory;

		private void Awake()
		{
			_gameFactory = AllServices.Container.Single<IGameFactory>();

			if (CheckInstantiateCharacter())
			{
				_gameFactory.CharacterCreated += OnCharacterCreated;
			}
			else
			{
				InitializeHeroTransform();
				_lastCharacterPosition = _characterTransform.position;
			}

			CreateArray();
		}

		private void OnEnable() =>
			PlayerMovedDistance += ShiftTiles;

		private void OnDisable() =>
			PlayerMovedDistance -= ShiftTiles;

		private void Update()
		{
			
			if(_characterTransform is null)
				return;
			
			Vector3 moveDirection = _characterTransform.position - _lastCharacterPosition;

			if (moveDirection.magnitude < _tileSize)
				return;

			ChooseDirection(moveDirection);
			Debug.Log(_lastCharacterPosition);
		}

		private void ChooseDirection(Vector3 moveDirection)
		{
			if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.z))
			{
				PlayerMovedDistance?.Invoke(moveDirection.x > 0 ? Direction.Right : Direction.Left);
				_lastCharacterPosition = GetLastCharacterPositionX(_characterTransform.position.x, _lastCharacterPosition.y, _lastCharacterPosition.z, _tileSize);
			}
			else
			{
				PlayerMovedDistance?.Invoke(moveDirection.z > 0 ? Direction.Up : Direction.Down);
				_lastCharacterPosition = GetLastCharacterPositionZ(_lastCharacterPosition.x, _lastCharacterPosition.y, _characterTransform.position.z, _tileSize);
			}
		}

		private void OnCharacterCreated() =>
			InitializeHeroTransform();

		private void ShiftTiles(Direction direction)
		{
			switch (direction)
			{
				case Direction.Right:
					Shift(Direction.Right);
					break;

				case Direction.Left:
					Shift(Direction.Left);
					break;

				case Direction.Up:
					Shift(Direction.Up);
					break;

				case Direction.Down:
					Shift(Direction.Down);
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
			}
		}

		private void Shift(Direction direction)
        {
            bool forward = direction is Direction.Up or Direction.Right;

            ShiftType shiftType = direction is Direction.Up or Direction.Down
                ? ShiftType.Row 
                : ShiftType.Column;

            Dictionary<Direction, Vector3> directionToVector = new Dictionary<Direction, Vector3>
            {
                [Direction.Up] = Vector3.forward,
                [Direction.Down] = Vector3.back,
                [Direction.Right] = Vector3.right,
                [Direction.Left] = Vector3.left
            };

            int gridSize = _tileGrid.GetLength(0);

            for (int x = 0; x < gridSize; x++)
            {
                int lastIndex = gridSize - 1;

                int indexFrom = forward ? lastIndex : 0;
                int indexTo = forward ? 0 : lastIndex;

                Vector3 positionShift = directionToVector[direction] * _moveDistance;

                Transform temp = GetValue(x, indexFrom, shiftType);

                for (int y = indexFrom; forward ? (y > indexTo) : (y < indexTo);)
                {
                    int nextIndex = forward
                        ? y - 1
                        : y + 1;

                    SetValue(x, y, shiftType, GetValue(x, nextIndex, shiftType));

                    if (forward)
                        y--;
                    else
                        y++;
                }

                SetValue(x, indexTo, shiftType, temp);
                temp.position += positionShift;
            }

        }

		private void InitializeHeroTransform() =>
			_characterTransform = _gameFactory.CharacterGameObject.transform;

		private bool CheckInstantiateCharacter() =>
			_gameFactory.CharacterGameObject is null;

		private Vector3 GetLastCharacterPositionX(float x, float y, float z, float tileSize) =>
			new Vector3(Mathf.Round(x / tileSize) * tileSize, y,z);

		private Vector3 GetLastCharacterPositionZ(float x, float y, float z, float tileSize) =>
			new Vector3(x, y, Mathf.Round(z / tileSize) * tileSize);

		private Transform GetValue(int x, int y, ShiftType shiftType) =>
			shiftType == ShiftType.Row ? _tileGrid[x, y] : _tileGrid[y, x];

		private void SetValue(int x, int y, ShiftType shiftType, Transform transformPosition)
        {
            if (shiftType == ShiftType.Row)
	            _tileGrid[x, y] = transformPosition;
            else
	            _tileGrid[y, x] = transformPosition;
        }

		private void CreateArray()
		{
			int gridSize = Mathf.RoundToInt(Mathf.Sqrt(_tiles.Length));
			_tileGrid = new Transform[gridSize, gridSize];

			for (int i = 0; i < _tiles.Length; i++)
			{
				int x = i / gridSize;
				int y = i % gridSize;
				_tileGrid[x, y] = _tiles[i];
			}
		}
	}
}