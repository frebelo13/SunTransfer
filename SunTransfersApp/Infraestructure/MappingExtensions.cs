using SunTransfersApp.Infraestructure.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunTransfersApp.Infraestructure
{
    public static class MappingExtensions
    {
        public static async Task SetProperty(this object obj, string propertyName, object propertyValue)
        {
            string[] childs = propertyName.Split('.');

            if (propertyValue == null)
            {
                return;
            }

            if (childs.Length == 1)
            {
                var prop = obj.GetType().GetProperty(propertyName);

                if (prop != null)
                {
                    prop.SetValue(obj, propertyValue);
                }
            }
            else
            {
                for (int i = 1; i < childs.Length; i++)
                {
                    var newObject = obj.GetType().GetProperty(childs[0]).GetValue(obj);
                    SetProperty(newObject, string.Join(".", childs.Skip(1)), propertyValue);
                }
            }

            return;
        }

        public static async Task SetFromCustomProperty(this object obj, string propertyName, object propertyValue)
        {
            string[] childs = propertyName.Split('.');

            if (propertyValue == null)
            {
                return;
            }

            if (childs.Length == 1)
            {
                var property = obj.GetType().GetProperties().Where(p => ((ColumnAttribute)(obj.GetType().GetProperty(p.Name).GetCustomAttributes(typeof(ColumnAttribute), true)[0])).Name == propertyName).FirstOrDefault();

                if (property != null)
                {
                    property.SetValue(obj, propertyValue);
                }

            }
            else
            {
                for (int i = 1; i < childs.Length; i++)
                {
                    var propName = obj.GetType().GetProperties().Where(p => ((ColumnAttribute)(obj.GetType().GetProperty(p.Name).GetCustomAttributes(typeof(ColumnAttribute), true)[0])).Name == childs[0]).FirstOrDefault();

                    var newObject = obj.GetType().GetProperty(propName.Name).GetValue(obj);

                    SetFromCustomProperty(newObject, string.Join(".", childs.Skip(1)), propertyValue);
                }
            }
        }
        public static void MapEndpoint(this WebApplication app)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var classes = assemblies.Distinct().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IEndpoints).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);


            foreach (var classe in classes)
            {
                var instance = Activator.CreateInstance(classe) as IEndpoints;
                instance?.ClientsEndpoints(app);
            }
        }
    }

}
