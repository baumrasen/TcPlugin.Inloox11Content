using System.Threading.Tasks;

namespace ConsoleApplicationExample
{
    class ConsoleApp
    {

        static async Task Main(string[] args)
        {

            int TestCase = 1;

            switch (TestCase)
            {
                case 0:
                    // do inloox client testing
                    await InlooxClientOnly.Main(args);
                    break;
                case 1:
                    // do tc plugin testing in console
                    await UsingTCpluginWithConsole.Main(args);
                    break;
                default:
                    break;
            }

        }

    }
}

