using CodeBase.Infrastracture.Services.Interfaces;

namespace CodeBase.Infrastracture.Services
{
	public class AllServices
	{
		private static AllServices _instance;

		public static AllServices Container => _instance ??= new AllServices();

		public void RegisterAsSingle<TService>(TService implementation) where TService : IService =>
			Implementation<TService>.ServiceInstance = implementation;

		public TService Single<TService>() where TService : IService =>
			Implementation<TService>.ServiceInstance;

		private static class Implementation<TService> where TService : IService
		{
			public static TService ServiceInstance;
		}
	}
}