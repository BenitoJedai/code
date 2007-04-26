using ScriptCoreLib;

using java.lang;

namespace java.util.logging
{
    [Script(IsNative = true)]
    public class Logger
    {
        #region methods
        /// <summary>
        /// Add a log Handler to receive logging messages.
        /// </summary>
        public void addHandler(Handler handler)
        {
        }

        /// <summary>
        /// Log a CONFIG message.
        /// </summary>
        public void config(string msg)
        {
        }

        /// <summary>
        /// Log a method entry.
        /// </summary>
        public void entering(string sourceClass, string sourceMethod)
        {
        }

        /// <summary>
        /// Log a method entry, with one parameter.
        /// </summary>
        public void entering(string sourceClass, string sourceMethod, object param1)
        {
        }

        /// <summary>
        /// Log a method entry, with an array of parameters.
        /// </summary>
        public void entering(string sourceClass, string sourceMethod, object[] @params)
        {
        }

        /// <summary>
        /// Log a method return.
        /// </summary>
        public void exiting(string sourceClass, string sourceMethod)
        {
        }

        /// <summary>
        /// Log a method return, with result object.
        /// </summary>
        public void exiting(string sourceClass, string sourceMethod, object result)
        {
        }

        /// <summary>
        /// Log a FINE message.
        /// </summary>
        public void fine(string msg)
        {
        }

        /// <summary>
        /// Log a FINER message.
        /// </summary>
        public void finer(string msg)
        {
        }

        /// <summary>
        /// Log a FINEST message.
        /// </summary>
        public void finest(string msg)
        {
        }

        /// <summary>
        /// Create an anonymous Logger.
        /// </summary>
        public static Logger getAnonymousLogger()
        {
            return default(Logger);
        }

        /// <summary>
        /// Create an anonymous Logger.
        /// </summary>
        public static Logger getAnonymousLogger(string resourceBundleName)
        {
            return default(Logger);
        }

        /// <summary>
        /// Get the current filter for this Logger.
        /// </summary>
        public Filter getFilter()
        {
            return default(Filter);
        }

        /// <summary>
        /// Get the Handlers associated with this logger.
        /// </summary>
        public Handler[] getHandlers()
        {
            return default(Handler[]);
        }

        /// <summary>
        /// Get the log Level that has been specified for this Logger.
        /// </summary>
        public Level getLevel()
        {
            return default(Level);
        }

        /// <summary>
        /// Find or create a logger for a named subsystem.
        /// </summary>
        public static Logger getLogger(string name)
        {
            return default(Logger);
        }

        /// <summary>
        /// Find or create a logger for a named subsystem.
        /// </summary>
        public static Logger getLogger(string name, string resourceBundleName)
        {
            return default(Logger);
        }

        /// <summary>
        /// Get the name for this logger.
        /// </summary>
        public string getName()
        {
            return default(string);
        }

        /// <summary>
        /// Return the parent for this Logger.
        /// </summary>
        public Logger getParent()
        {
            return default(Logger);
        }

        /// <summary>
        /// Retrieve the localization resource bundle for this logger for the current default locale.
        /// </summary>
        public ResourceBundle getResourceBundle()
        {
            return default(ResourceBundle);
        }

        /// <summary>
        /// Retrieve the localization resource bundle name for this logger.
        /// </summary>
        public string getResourceBundleName()
        {
            return default(string);
        }

        /// <summary>
        /// Discover whether or not this logger is sending its output to its parent logger.
        /// </summary>
        public bool getUseParentHandlers()
        {
            return default(bool);
        }

        /// <summary>
        /// Log an INFO message.
        /// </summary>
        public void info(string msg)
        {
        }

        /// <summary>
        /// Check if a message of the given level would actually be logged by this logger.
        /// </summary>
        public bool isLoggable(Level level)
        {
            return default(bool);
        }

        /// <summary>
        /// Log a message, with no arguments.
        /// </summary>
        public void log(Level level, string msg)
        {
        }

        /// <summary>
        /// Log a message, with one object parameter.
        /// </summary>
        public void log(Level level, string msg, object param1)
        {
        }

        /// <summary>
        /// Log a message, with an array of object arguments.
        /// </summary>
        public void log(Level level, string msg, object[] @params)
        {
        }

        /// <summary>
        /// Log a message, with associated Throwable information.
        /// </summary>
        public void log(Level level, string msg, Throwable thrown)
        {
        }

        /// <summary>
        /// Log a LogRecord.
        /// </summary>
        public void log(LogRecord record)
        {
        }

        /// <summary>
        /// Log a message, specifying source class and method, with no arguments.
        /// </summary>
        public void logp(Level level, string sourceClass, string sourceMethod, string msg)
        {
        }

        /// <summary>
        /// Log a message, specifying source class and method, with a single object parameter to the log message.
        /// </summary>
        public void logp(Level level, string sourceClass, string sourceMethod, string msg, object param1)
        {
        }

        /// <summary>
        /// Log a message, specifying source class and method, with an array of object arguments.
        /// </summary>
        public void logp(Level level, string sourceClass, string sourceMethod, string msg, object[] @params)
        {
        }

        /// <summary>
        /// Log a message, specifying source class and method, with associated Throwable information.
        /// </summary>
        public void logp(Level level, string sourceClass, string sourceMethod, string msg, Throwable thrown)
        {
        }

        /// <summary>
        /// Log a message, specifying source class, method, and resource bundle name with no arguments.
        /// </summary>
        public void logrb(Level level, string sourceClass, string sourceMethod, string bundleName, string msg)
        {
        }

        /// <summary>
        /// Log a message, specifying source class, method, and resource bundle name, with a single object parameter to the log message.
        /// </summary>
        public void logrb(Level level, string sourceClass, string sourceMethod, string bundleName, string msg, object param1)
        {
        }

        /// <summary>
        /// Log a message, specifying source class, method, and resource bundle name, with an array of object arguments.
        /// </summary>
        public void logrb(Level level, string sourceClass, string sourceMethod, string bundleName, string msg, object[] @params)
        {
        }

        /// <summary>
        /// Log a message, specifying source class, method, and resource bundle name, with associated Throwable information.
        /// </summary>
        public void logrb(Level level, string sourceClass, string sourceMethod, string bundleName, string msg, Throwable thrown)
        {
        }

        /// <summary>
        /// Remove a log Handler.
        /// </summary>
        public void removeHandler(Handler handler)
        {
        }

        /// <summary>
        /// Set a filter to control output on this Logger.
        /// </summary>
        public void setFilter(Filter newFilter)
        {
        }

        /// <summary>
        /// Set the log level specifying which message levels will be logged by this logger.
        /// </summary>
        public void setLevel(Level newLevel)
        {
        }

        /// <summary>
        /// Set the parent for this Logger.
        /// </summary>
        public void setParent(Logger parent)
        {
        }

        /// <summary>
        /// Specify whether or not this logger should send its output to it's parent Logger.
        /// </summary>
        public void setUseParentHandlers(bool useParentHandlers)
        {
        }

        /// <summary>
        /// Log a SEVERE message.
        /// </summary>
        public void severe(string msg)
        {
        }

        /// <summary>
        /// Log throwing an exception.
        /// </summary>
        public void throwing(string sourceClass, string sourceMethod, Throwable thrown)
        {
        }

        /// <summary>
        /// Log a WARNING message.
        /// </summary>
        public void warning(string msg)
        {
        }

        #endregion

    }
}
