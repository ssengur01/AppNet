namespace AppNET.Infrastructure
{
    public class IOCContainer
    {
        private static readonly Dictionary<Type, Func<object>> container =
            new Dictionary<Type, Func<object>>();

        public static T Resolve<T>()
        {
            var keyTipi=typeof(T);
            var metot = container[keyTipi];
            var nesne = metot();
            var donusTipi = (T)nesne;
            return donusTipi;


            //return (T)container[typeof(T)]();
        }
        public static void Register<T>(Func<object> func)
        {
            if(container.ContainsKey(typeof(T)))
              container.Remove(typeof(T));

            container.Add(typeof(T), func);
        }
    }
}