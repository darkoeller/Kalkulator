using System;
using System.Collections.Generic;
using System.Reflection;
using NLog;
using PostSharp.Aspects;

namespace ObracunPlace
{
    [Serializable]  
    public class NLogAttribute : OnMethodBoundaryAspect  
    {  
        //Logger used to log the messages. The NLog logger is not serializable, so this won't be serialized. (this does not impact functionality!) 
        [NonSerialized] private Logger _logger;

        private List<ParameterInfo> _parameterInfos;  
        //Name of the method, initialized at compile time to improve performance!  
        private string _methodName;  
        //Name of the class, initialized at compile time to improve performance!  
        private string _className;

        //Intialize some fields at compile time to improve performance. 
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)  
        {  
            base.CompileTimeInitialize(method, aspectInfo);  
            //Compile Time initizalization of the class name.  
            if (method.ReflectedType != null) _className = method.ReflectedType.Name;
            //Compile Time initizalization of the method name.  
            _methodName = _className + "." + method.Name;  
            _parameterInfos = new List<ParameterInfo>(method.GetParameters());  
        }  
        public override void OnEntry(MethodExecutionArgs args)  
        {  
            base.OnEntry(args);  
            _logger = LogManager.GetCurrentClassLogger();  
            var logMessage = "Entering " + _methodName + " with arguments: ";  
            logMessage = AddArgumentsToLogMessage(args, logMessage);  
            _logger.Debug(logMessage);  
        } 

        //This implementation can be improved by using a string builder.
        //Also you might be interested in checking if the argument is an IEnumerable and log this using a specific syntax.  
        private string AddArgumentsToLogMessage(MethodExecutionArgs args, string logMessage)  
        {  
            var argumentIndex = 0;  
            foreach (var argument in args.Arguments)  
            {  
                var argumentInfo = _parameterInfos[argumentIndex];  
                logMessage += argumentInfo.Name + " = { ";  
                if (argument != null)  
                    logMessage += argument.ToString();  
                else  
                    logMessage += "[null]";  
                logMessage += " }" + " ";  
                argumentIndex++;  
            }  
            return logMessage;  
        }  
        public override void OnExit(MethodExecutionArgs args)  
        {  
            base.OnExit(args);  
            var logMessage = "Izlaz " + _methodName + " sa argumentima: ";  
            logMessage = AddArgumentsToLogMessage(args, logMessage);  
            logMessage += "Rezultat: ";  
            if(args.ReturnValue == null)  
                logMessage += "[null]";  
            else  
                logMessage += args.ReturnValue.ToString();  
            _logger.Debug(logMessage);  
        }  
        public override void OnException(MethodExecutionArgs args)  
        {  
            base.OnException(args);  
            var logMessage = "Pojavila se greška " + _methodName + " sa argumentima: ";  
            logMessage = AddArgumentsToLogMessage(args, logMessage);  
            logMessage += "Greška je u: " + args.Exception.Message;  
            _logger.Error(logMessage);  
            args.FlowBehavior = FlowBehavior.RethrowException;  
        }  
    }
}  
  