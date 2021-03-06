﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServicesAccessUbicar.cs.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Message {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Message() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ServicesAccessUbicar.cs.Resources.Message", typeof(Message).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error in communication, please validate your internet connection and try again, if the error persists try later..
        /// </summary>
        internal static string ComunicationError {
            get {
                return ResourceManager.GetString("ComunicationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Impossible to establish connection with the server, validate your connection to the internet, if the error persists try later.
        /// </summary>
        internal static string ConnectFailure {
            get {
                return ResourceManager.GetString("ConnectFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The maximum length of the request has been exceeded.
        /// </summary>
        internal static string MessageLengthLimitExceeded {
            get {
                return ResourceManager.GetString("MessageLengthLimitExceeded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The resource you are trying to access was not found.
        /// </summary>
        internal static string NotFound {
            get {
                return ResourceManager.GetString("NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The request has been canceled.
        /// </summary>
        internal static string RequestCancel {
            get {
                return ResourceManager.GetString("RequestCancel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error has occurred in the request.
        /// </summary>
        internal static string RequestError {
            get {
                return ResourceManager.GetString("RequestError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The waiting time for the request was exceeded.
        /// </summary>
        internal static string RequestTimeout {
            get {
                return ResourceManager.GetString("RequestTimeout", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An internal error occurred on the server.
        /// </summary>
        internal static string ServerInternalError {
            get {
                return ResourceManager.GetString("ServerInternalError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service error, try again if the error persists try later.
        /// </summary>
        internal static string ServiceError {
            get {
                return ResourceManager.GetString("ServiceError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The server is not available at this time, please try later..
        /// </summary>
        internal static string ServiceUnavailable {
            get {
                return ResourceManager.GetString("ServiceUnavailable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You are not authorized to access the service.
        /// </summary>
        internal static string Unauthorized {
            get {
                return ResourceManager.GetString("Unauthorized", resourceCulture);
            }
        }
    }
}
