using HttpWarmUp.Uwp;
using System;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace HttpWarmpUp
{
    public  class Log
    {
        Console Console;
        public Log(Console c)
        {
            Console = c;
        }
       
        public  StringBuilder sbWarnings = new StringBuilder();
        public  StringBuilder sbErrors = new StringBuilder();
        public  StringBuilder sbInfo = new StringBuilder();

        public void WriteTabbedWarning(int tabCount, string text, params object[] args)
        {
            //ConsoleColor currentColor = Console.ForegroundColor;
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.Out.Write(new String('\t', tabCount));
            //Console.Out.WriteLine(text, args);
            //Console.ForegroundColor = currentColor;

            Console.Write(new String('\t', tabCount));
            Console.WriteLine(text, args);

            sbWarnings.AppendLine(String.Format(text, args));   
        }

        public  void FlushInfoToEventLog()
        {
            try
            {
                    string logData = GetLogData(sbInfo);
                    this.WriteTabbedInfo(0, "Registered Info Messages:\n{0}", logData);
            }
            catch(Exception ex)
            {
                this.WriteTabbedError(0, "No se han podido escribir los eventos Informativos al visor de enventos. Revise la configuración. Sección: <system.diagnostics>. Más información en: http://msdn.microsoft.com/en-us/library/1txedc80(VS.71).aspx\n\nExcepción que se ha producido:{0}", ex.ToString());

            }
        }

        public  void FlushWarningsToEventLog()
        {
            try
            {
                    string logData = GetLogData(sbWarnings);
                    this.WriteTabbedWarning(0,"Registered Warnings:\n{0}", logData);
               
            }
            catch (Exception ex)
            {
                this.WriteTabbedError(0, "No se han podido escribir los eventos de Aviso al visor de enventos. Revise la configuración. Sección: <system.diagnostics>. Más información en: http://msdn.microsoft.com/en-us/library/1txedc80(VS.71).aspx\n\nExcepción que se ha producido:{0}", ex.ToString());

            }
        }

        public  void FlushErrorsToEventLog()
        {
            try
            {
                    string logData = GetLogData(sbErrors);
                    this.WriteTabbedError(0, "Registered Errors:\n{0}", logData);
               
            }
            catch (Exception ex)
            {
                this.WriteTabbedError(0, "No se han podido escribir los eventos de Error al visor de enventos. Revise la configuración. Sección: <system.diagnostics>. Más información en: http://msdn.microsoft.com/en-us/library/1txedc80(VS.71).aspx\n\nExcepción que se ha producido:{0}", ex.ToString());

            }
        }

        private  string GetLogData(StringBuilder sb)
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

        public  void WriteTabbedInfo(int tabCount, string text, params object[] args)
        {
            //ConsoleColor currentColor = Console.ForegroundColor;
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.Out.Write(new String('\t', tabCount));
            //Console.Out.WriteLine(text, args);
            //Console.ForegroundColor = currentColor;
            // Console.Text += uwpNewLine;

            Console.Write(new String('\t', tabCount));
            Console.WriteLine(text, args);

            sbInfo.AppendLine(String.Format(text, args));
        }

        public  void WriteTabbedError(int tabCount, string text, params object[] args)
        {
            /*
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Out.Write(new String('\t', tabCount));
            Console.Out.WriteLine(text, args);
            Console.ForegroundColor = currentColor;
            */

            Console.Write(new String('\t', tabCount));
            Console.WriteLine(text, args);

            sbErrors.AppendLine(String.Format(text, args));
        }

        public  void WriteTabbedText(int tabCount, string text, params object[] args)
        {
            /*
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Out.Write(new String('\t', tabCount));
            Console.Out.WriteLine(text, args);
            Console.ForegroundColor = currentColor;
            */

            Console.Write(new String('\t', tabCount));
            Console.WriteLine(text, args);

            sbInfo.AppendLine(String.Format(text, args));
        }
    }
}
