﻿#region CVS Version Header
/*
 * $Id$
 * Last modified by $Author$
 * Last modified at $Date$
 * $Revision$
 */
#endregion

using System;
using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;
using log4net.Repository.Hierarchy;

namespace RssBandit.Common.Logging {
	

	/// <summary>
	///  Wrapper around Log4Net.
	///  For configuration options have a look to http://logging.apache.org/log4net/release/manual/configuration.html.
	///  For configuration examples have a look to http://logging.apache.org/log4net/release/config-examples.html.
	/// </summary>
	internal class Log {

		private static ILog Logger;

		private static string LOGCONFIG = "RssBandit.exe.log4net.config";
		/// <summary>
		/// statick initializer
		/// </summary>
		static Log() {
            // Set up a simple configuration that logs on the console.

            var hierarchy = LogManager.GetRepository(Assembly.GetEntryAssembly());



            if ( File.Exists(Log4NetConfigFile)) 
            {
                // Env variable isn't expanded, do it manually
                var configfile = File.ReadAllText(Log4NetConfigFile);
                // insert the alt path
                var newConfig = configfile.Replace(@"${APPDATA}", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
#if ALT_CONFIG_PATH

                // insert the alt path
                var newConfig = newConfig.Replace(@"\\RssBandit\\", @"\\RssBandit\\Debug\\");
#endif
                XmlConfigurator.Configure(hierarchy, new MemoryStream(System.Text.Encoding.UTF8.GetBytes(newConfig)));
			}
			else {
                BasicConfigurator.Configure(hierarchy);
			}
            
			Logger = GetLogger(typeof(Log));
		}

		/// <summary>
		///  The Full Path to the Config File
		/// </summary>
		public static string Log4NetConfigFile {
			get { 
				return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) , LOGCONFIG); 
			}
		}

		/// <summary>
		/// Log an error. If you don't want to use your own ILog variable returned by
		/// <see cref="GetLogger">GetLogger</see>, you can call this method.
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="exception">Exception</param>
		/// <remarks>The class information included always refer to <c>Common.Logging</c>.</remarks>
		public static void Error(string message,Exception exception) {
			try {
				Logger.Error(message,exception);
			}
			catch(Exception e) {
				throw new ApplicationException(String.Format("Logger.Error() failed on logging '{0}'.", message +"::"+ exception.Message), e);
			}		
		}

		/// <summary>
		/// Log an error. If you don't want to use your own ILog variable returned by
		/// <see cref="GetLogger">GetLogger</see>, you can call this method.
		/// </summary>
		/// <param name="message">Message</param>
		public static void Error(string message) {
			try {
				Logger.Error(message);
			}
			catch(Exception e) {
				throw new ApplicationException(String.Format("Logger.Error() failed on logging '{0}'.", message ), e);
			}
		}
 
		/// <summary>
		/// Log a warning. If you don't want to use your own ILog variable returned by
		/// <see cref="GetLogger">GetLogger</see>, you can call this method.
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="exception">Exception</param>
		public static void Warning(string message,Exception exception) {
			try {
				Logger.Warn(message,exception);
			}
			catch(Exception e) {
				throw new ApplicationException(String.Format("Logger.Warn() failed on logging '{0}'.", message +"::"+ exception.Message), e);
			}
		}

		/// <summary>
		/// Log a warning. If you don't want to use your own ILog variable returned by
		/// <see cref="GetLogger">GetLogger</see>, you can call this method.
		/// </summary>
		/// <param name="message">Message</param>
		public static void Warning(string message) {
			try {
				Logger.Warn(message);
			}
			catch(Exception e) {
				throw new ApplicationException(String.Format("Logger.Warn() failed on logging '{0}'.", message), e);
			}
		}

		/// <summary>
		/// Log a fatal error. If you don't want to use your own ILog variable returned by
		/// <see cref="GetLogger">GetLogger</see>, you can call this method.
		/// </summary>
		/// <param name="message"> Message</param>
		/// <param name="exception">Exception</param>
		public static void Fatal(string message,Exception exception) {
			try {
				Logger.Fatal(message,exception);
			}
			catch(Exception e) {
				throw new ApplicationException(String.Format("Logger.Fatal() failed on logging '{0}'.", message +"::"+ exception.Message), e);
			}
		}

		/// <summary>
		/// Log a fatal error. If you don't want to use your own ILog variable returned by
		/// <see cref="GetLogger">GetLogger</see>, you can call this method.
		/// </summary>
		/// <param name="message">Message </param>
		public static void Fatal(string message) {
			try {
				Logger.Fatal(message);
			}
			catch(Exception e) {
				throw new ApplicationException(String.Format("Logger.Fatal() failed on logging '{0}'.", message), e);
			}
		}

		/// <summary>
		/// Log an info error. If you don't want to use your own ILog variable returned by
		/// <see cref="GetLogger">GetLogger</see>, you can call this method.
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="exception">Exception</param>
		public static void Info(string message,Exception exception) {
			try {
				Logger.Info(message,exception);
			}
			catch(Exception e) {
				throw new ApplicationException(String.Format("Logger.Info() failed on logging '{0}'.", message +"::"+ exception.Message), e);
			}
		}

		/// <summary>
		///  Log a info message. If you don't want to use your own ILog variable returned by
		/// <see cref="GetLogger">GetLogger</see>, you can call this method.
		/// </summary>
		/// <param name="message">Message</param>
		public static void Info(string message) {
			try {
				Logger.Info(message);
			}
			catch(Exception e) {
				throw new ApplicationException(String.Format("Logger.Info() failed on logging '{0}'.", message), e);
			}
		}

		/// <summary>
		///  Log a trace message. If you don't want to use your own ILog variable returned by
		/// <see cref="GetLogger">GetLogger</see>, you can call this method.
		/// </summary>
		/// <param name="message">Message</param>
		public static void Trace(string message) {
			Log.Info(message);
		}
		
		/// <summary>
		///  Log a trace message. If you don't want to use your own ILog variable returned by
		/// <see cref="GetLogger">GetLogger</see>, you can call this method.
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="exception">Exception to trace</param>
		public static void Trace(string message, Exception exception) {
			Log.Info(message, exception);
		}

		/// <summary>
		/// Log an debug error. If you don't want to use your own ILog variable returned by
		/// <see cref="GetLogger">GetLogger</see>, you can call this method.
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="exception">Exception</param>
		public static void Debug(string message,Exception exception) {
			try {
				Logger.Debug(message,exception);
			}
			catch(Exception e) {
				throw new ApplicationException(String.Format("Logger.Debug() failed on logging '{0}'.", message +"::"+ exception.Message), e);
			}
		}

		/// <summary>
		///  Log a debug message. If you don't want to use your own ILog variable returned by
		/// <see cref="GetLogger">GetLogger</see>, you can call this method.
		/// </summary>
		/// <param name="message">Message</param>
		public static void Debug(string message) {
			try {
				Logger.Debug(message);
			}
			catch(Exception e) {
				throw new ApplicationException(String.Format("Logger.Debug() failed on logging '{0}'.", message), e);
			}
		}

		/// <summary>
		///  If you want to use your own ILog variable in your class, initialize
		///  them with a instance returned by this method.
		/// </summary>
		/// <param name="type">System.Type</param>
		/// <returns>Instance of a logger impl. ILog</returns>
		public static ILog GetLogger(System.Type type) {
			return LogManager.GetLogger(type);
		}

	}
}
