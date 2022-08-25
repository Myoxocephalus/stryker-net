using System;

namespace Stryker
{
    public sealed class MutantContext: IDisposable
    {
        [ThreadStatic] private static int depth;

        public MutantContext()
        {
            depth++; //NOSONAR
        }

        public void Dispose()
        {
            depth--; //NOSONAR
        }

        public static bool InStatic()
        {
            return depth > 0;
        }

        public static T TrackValue<T>(Func<T> builder)
        {
            using (MutantContext context = new MutantContext())
            {
                return builder();
            }
        }
    }
}
