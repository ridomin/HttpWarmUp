using System;
using System.Text;

namespace HttpWarmpUp
{
    public static class Log
    {
      
        public static StringBuilder sbWarnings = new StringBuilder();
        public static StringBuilder sbErrors = new StringBuilder();
        public static StringBuilder sbInfo = new StringBuilder();

        public static void WriteTabbedWarning(int tabCount, string text, params object[] args)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Out.Write(new String('\t', tabCount));
            Console.Out.WriteLine(text, args);
            Console.ForegroundColor = currentColor;

            sbWarnings.AppendLine(String.Format(text, args));   
        }

        public static void FlushInfoToEventLog()
        {
            try
            {
                    string logData = GetLogData(sbInfo);
                    Log.WriteTabbedInfo(0, "Registered Info Messages:\n{0}", logData);
            }
            catch(Exception ex)
            {
                Log.WriteTabbedError(0, "No se han podido escribir los eventos Informativos al visor de enventos. Revise la configuración. Sección: <system.diagnostics>. Más información en: http://msdn.microsoft.com/en-us/library/1txedc80(VS.71).aspx\n\nExcepción que se ha producido:{0}", ex.ToString());

            }
        }

        public static void FlushWarningsToEventLog()
        {
            try
            {
                    string logData = GetLogData(sbWarnings);
                    Log.WriteTabbedWarning(0,"Registered Warnings:\n{0}", logData);
               
            }
            catch (Exception ex)
            {
                Log.WriteTabbedError(0, "No se han podido escribir los eventos de Aviso al visor de enventos. Revise la configuración. Sección: <system.diagnostics>. Más información en: http://msdn.microsoft.com/en-us/library/1txedc80(VS.71).aspx\n\nExcepción que se ha producido:{0}", ex.ToString());

            }
        }

        public static void FlushErrorsToEventLog()
        {
            try
            {
                    string logData = GetLogData(sbErrors);
                    Log.WriteTabbedError(0, "Registered Errors:\n{0}", logData);
               
            }
            catch (Exception ex)
            {
                Log.WriteTabbedError(0, "No se han podido escribir los eventos de Error al visor de enventos. Revise la configuración. Sección: <system.diagnostics>. Más información en: http://msdn.microsoft.com/en-us/library/1txedc80(VS.71).aspx\n\nExcepción que se ha producido:{0}", ex.ToString());

            }
        }

        private static string GetLogData(StringBuilder sb)
        {
            string logData = String.Empty;
            if (sb.Length > 15000)
            {
                logData = sb.ToString().Substring(0, 15000) + "[... truncated ...]";
            }
            else
            {
                logData = sb.ToString();
            }
            return logData;
        }

        public static void WriteTabbedInfo(int tabCount, string text, params object[] args)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Out.Write(new String('\t', tabCount));
            Console.Out.WriteLine(text, args);
            Console.ForegroundColor = currentColor;
            sbInfo.AppendLine(String.Format(text, args));
        }

        public static void WriteTabbedError(int tabCount, string text, params object[] args)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Out.Write(new String('\t', tabCount));
            Console.Out.WriteLine(text, args);
            Console.ForegroundColor = currentColor;

            sbErrors.AppendLine(String.Format(text, args));
        }

        public static void WriteTabbedText(int tabCount, string text, params object[] args)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Out.Write(new String('\t', tabCount));
            Console.Out.WriteLine(text, args);
            Console.ForegroundColor = currentColor;

            sbInfo.AppendLine(String.Format(text, args));
        }
    }
}
