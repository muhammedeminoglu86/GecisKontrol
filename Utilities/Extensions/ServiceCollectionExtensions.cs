using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Utilities.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddScopedServicesFromAssembly(
			this IServiceCollection services,
			Assembly assembly,
			string interfaceNamespacePrefix,
			string implementationNamespacePrefix)
		{
			var interfaceTypes = assembly.GetTypes()
				.Where(t => t.IsInterface && t.Namespace != null && t.Namespace.StartsWith(interfaceNamespacePrefix));

			foreach (var interfaceType in interfaceTypes)
			{
				var implementationType = assembly.GetTypes()
					.FirstOrDefault(t => t.IsClass && !t.IsAbstract && t.Namespace != null && t.Namespace.StartsWith(implementationNamespacePrefix) && interfaceType.IsAssignableFrom(t));

				if (implementationType != null)
				{
					services.AddTransient(interfaceType, implementationType);
				}
			}

			return services;
		}
	}
}
