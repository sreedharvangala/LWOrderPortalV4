﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Configuration;
using System.IO.Compression;
using System.Xml.XPath;
using System.Web.Routing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Finsoft.Data;
using Finsoft.Handlers;
using Finsoft.Web;

namespace Finsoft.Services
{
	public abstract class ServiceRequestHandler
    {
        
        public virtual string[] AllowedMethods
        {
            get
            {
                return new string[] {
                        "POST"};
            }
        }
        
        public virtual bool RequiresAuthentication
        {
            get
            {
                return false;
            }
        }
        
        public abstract object HandleRequest(DataControllerService service, JObject args);
        
        public virtual object HandleException(JObject args, Exception ex)
        {
            return ApplicationServices.Current.HandleException(args, ex);
        }
        
        public static void Redirect(string redirectUrl)
        {
            throw new ServiceRequestRedirectException(redirectUrl);
        }
    }
    
    public class GetPageServiceRequestHandler : ServiceRequestHandler
    {
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            var r = args["request"].ToObject<PageRequest>();
            return service.GetPage(ControllerUtilities.ValidateName(((string)(args["controller"]))), ControllerUtilities.ValidateName(((string)(args["view"]))), r);
        }
    }
    
    public class GetControllerListServiceRequestHandler : ServiceRequestHandler
    {
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            var jsonArray = new StringBuilder("[");
            var list = args["controllers"].ToObject<string[]>();
            var first = true;
            foreach (var name in list)
            {
                if (first)
                	first = false;
                else
                	jsonArray.Append(",");
                var config = DataControllerBase.CreateConfigurationInstance(GetType(), name);
                var json = config.ToJson();
                jsonArray.Append(json);
            }
            jsonArray.Append("]");
            return jsonArray.ToString();
        }
    }
    
    public class CommitServiceRequestHandler : ServiceRequestHandler
    {
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            var tm = new TransactionManager();
            return tm.Commit(((JArray)(args["log"])));
        }
    }
    
    public class GetPageListServiceRequestHandler : ServiceRequestHandler
    {
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            return service.GetPageList(args["requests"].ToObject<PageRequest[]>());
        }
    }
    
    public class GetListOfValuesServiceRequestHandler : ServiceRequestHandler
    {
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            var r = args["request"].ToObject<DistinctValueRequest>();
            return service.GetListOfValues(ControllerUtilities.ValidateName(((string)(args["controller"]))), ControllerUtilities.ValidateName(((string)(args["view"]))), r);
        }
    }
    
    public class ExecuteServiceRequestHandler : ServiceRequestHandler
    {
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            var a = args["args"].ToObject<ActionArgs>();
            return service.Execute(ControllerUtilities.ValidateName(((string)(args["controller"]))), ControllerUtilities.ValidateName(((string)(args["view"]))), a);
        }
    }
    
    public class ExecuteAndGetPageServiceRequestHandler : ServiceRequestHandler
    {
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            var arg = args.ToObject<ExecuteViewPageArgs>();
            var a = arg.Args;
            var result = service.Execute(a.Controller, a.View, a);
            if (result.Errors.Count > 0)
            {
                var vp = new ViewPage();
                vp.Errors = result.Errors;
                return vp;
            }
            else
            {
                var request = new PageRequest(0, arg.PageSize, string.Empty, null);
                request.Controller = a.Controller;
                request.View = a.View;
                request.LastCommandName = a.CommandName;
                request.LastCommandArgument = a.CommandArgument;
                request.RequiresMetaData = arg.Metadata;
                request.DoesNotRequireAggregates = !arg.Aggregates;
                request.RequiresRowCount = arg.RowCount;
                request.SyncKey = GetPrimaryKey(result, a);
                return service.GetPage(a.Controller, a.View, request);
            }
        }
        
        private object[] GetPrimaryKey(ActionResult result, ActionArgs args)
        {
            var config = Controller.CreateConfigurationInstance(GetType(), args.Controller);
            var pKeys = new SortedDictionary<string, FieldValue>();
            foreach (XPathNavigator nav in config.Select("/c:dataController/c:fields/c:field[@isPrimaryKey=\'true\']"))
            {
                foreach (var fvo in result.Values)
                	if (fvo.Name == nav.GetAttribute("name", string.Empty))
                    {
                        pKeys[fvo.Name] = fvo;
                        break;
                    }
                foreach (var fvo in args.Values)
                	if (fvo.Name == nav.GetAttribute("name", string.Empty))
                    {
                        pKeys[fvo.Name] = fvo;
                        break;
                    }
            }
            var key = new List<object>();
            foreach (var fvo in pKeys.Values)
            	key.Add(fvo.Value);
            return key.ToArray();
        }
    }
    
    public class ExecuteListServiceRequestHandler : ServiceRequestHandler
    {
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            return service.ExecuteList(args["requests"].ToObject<ActionArgs[]>());
        }
    }
    
    public class GetCompletionListServiceRequestHandler : ServiceRequestHandler
    {
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            return service.GetCompletionList(((string)(args["prefixText"])), ((int)(args["count"])), ((string)(args["contextKey"])));
        }
    }
    
    public class LoginServiceRequestHandler : ServiceRequestHandler
    {
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            return service.Login(((string)(args["username"])), ((string)(args["password"])), ((bool)(args["createPersistentCookie"])));
        }
    }
    
    public class LogoutServiceRequestHandler : ServiceRequestHandler
    {
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            service.Logout();
            return null;
        }
    }
    
    public class RolesServiceRequestHandler : ServiceRequestHandler
    {
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            return service.Roles();
        }
    }
    
    public class AddonServiceRequestHandler : ServiceRequestHandler
    {
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            var type = ((string)(args["type"]));
            var method = ((string)(args["method"]));
            object result = null;
            foreach (var addon in ApplicationServices.Addons)
            {
                var t = addon.GetType();
                if (t.Name == type)
                {
                    result = t.GetMethod("Invoke").Invoke(addon, new object[] {
                                service,
                                method,
                                args["args"]});
                    break;
                }
            }
            return result;
        }
    }
    
    public class ThemesServiceRequestHandler : ServiceRequestHandler
    {
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            return service.Themes();
        }
    }
    
    public class SavePermalinkServiceRequestHandler : ServiceRequestHandler
    {
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            service.SavePermalink(((string)(args["link"])), ((string)(args["html"])));
            return null;
        }
    }
    
    public class EncodePermalinkServiceRequestHandler : ServiceRequestHandler
    {
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            return service.EncodePermalink(((string)(args["link"])), ((bool)(args["rooted"])));
        }
    }
    
    public class ListAllPermalinksServiceRequestHandler : ServiceRequestHandler
    {
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            return service.ListAllPermalinks();
        }
    }
    
    public class GetSurveyServiceRequestHandler : ServiceRequestHandler
    {
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            return service.GetSurvey(((string)(args["name"])));
        }
    }
    
    public class DnnOAuthServiceRequestHandler : ServiceRequestHandler
    {
        
        public override string[] AllowedMethods
        {
            get
            {
                return new string[] {
                        "GET",
                        "POST"};
            }
        }
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            var handler = new DnnOAuthHandler();
            handler.ProcessRequest(HttpContext.Current);
            return null;
        }
        
        public override object HandleException(JObject args, Exception ex)
        {
            if (ex is ThreadAbortException)
            	throw ex;
            return base.HandleException(args, ex);
        }
    }
    
    public class GetIdentityServiceRequestHandler : ServiceRequestHandler
    {
        
        public override string[] AllowedMethods
        {
            get
            {
                return new string[] {
                        "GET",
                        "POST"};
            }
        }
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            var user = Membership.GetUser();
            var res = HttpContext.Current.Response;
            if (!ApplicationServicesBase.AuthorizationIsSupported)
            {
                res.Write("<h1>This app does not have a built-in security system and cannot run in native mo" +
                        "de. Add membership support to the app.</h1>");
                res.End();
            }
            if ((user == null) || !((HttpContext.Current.Request.QueryString["force"] == "false")))
            {
                FormsAuthentication.SignOut();
                var returnUrl = (HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/_invoke/getidentity?force=false");
                res.Redirect(string.Format("{0}?ReturnUrl={1}&_accMan=login", FormsAuthentication.LoginUrl, HttpUtility.UrlEncode(returnUrl)), true);
            }
            else
            {
                var ticket = ApplicationServices.Current.CreateTicket(user, null);
                ticket.Claims.Add("deviceId", Guid.NewGuid().ToString().Replace("-", string.Empty));
                ticket.Claims.Add("culture", CultureInfo.CurrentCulture.Name);
                res.Clear();
                res.Write("<html><body><script>");
                var token = JsonConvert.SerializeObject(ticket, Formatting.None);
                var userAgent = HttpContext.Current.Request.UserAgent;
                if (userAgent.Contains("UMA-iOS") || userAgent.Contains("UMA-OSX"))
                {
                    res.Write("window.webkit.messageHandlers.invoke.postMessage({ method: \'addidentity\', args: ");
                    res.Write(token);
                    res.Write("});");
                }
                else
                	if (userAgent.Contains("UMA-W7"))
                    {
                        res.Write("CefSharp.BindObjectAsync(\'CloudOnTime\').then(function(){window.CloudOnTime.invoke" +
                                "(\'addidentity\', \'");
                        res.Write(HttpUtility.UrlPathEncode(token));
                        res.Write("\')});");
                    }
                    else
                    {
                        res.Write("function add() {window.CloudOnTime.invoke(\'addidentity\', \'");
                        res.Write(HttpUtility.UrlPathEncode(token));
                        res.Write("\');}if(typeof(CefSharp)!=\'undefined\')CefSharp.BindObjectAsync(\'CloudOnTime\').then" +
                                "(add);else add();");
                    }
                res.Write("</script></body></html>");
                res.End();
            }
            return null;
        }
        
        public override object HandleException(JObject args, Exception ex)
        {
            if (ex is ThreadAbortException)
            	return base.HandleException(args, ex);
            throw ex;
        }
    }
    
    public class GetManifestServiceRequestHandler : ServiceRequestHandler
    {
        
        public override string[] AllowedMethods
        {
            get
            {
                return new string[] {
                        "GET",
                        "POST"};
            }
        }
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            var services = ApplicationServices.Current;
            var response = new JObject();
            var settings = services.DefaultSettings;
            response["name"] = ApplicationServices.TryGetJsonProperty(settings, "appName");
            if (string.IsNullOrEmpty(((string)(response["name"]))))
            	response["name"] = services.Name;
            response["appVersion"] = ApplicationServices.Version;
            response["hostVersion"] = ApplicationServices.HostVersion;
            response["copyright"] = Site.Copyright;
            response["home"] = services.UserHomePageUrl();
            response["icon"] = ApplicationServices.TryGetJsonProperty(settings, "host.icon");
            response["color"] = ApplicationServices.TryGetJsonProperty(settings, "host.color");
            response["image"] = ApplicationServices.TryGetJsonProperty(settings, "host.image");
            response["description"] = ApplicationServices.TryGetJsonProperty(settings, "host.description");
            response["defaultTheme"] = ApplicationServices.TryGetJsonProperty(settings, "ui.theme.name");
            response["defaultAccent"] = ApplicationServices.TryGetJsonProperty(settings, "ui.theme.accent");
            var themeInfo = services.UserThemes();
            response["themes"] = themeInfo["themes"];
            response["accents"] = themeInfo["accents"];
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                var files = new List<ManifestFile>();
                var reqFiles = new List<ManifestFile>();
                // enumerate files to download
                AddSiteFiles(files, "css");
                AddSiteFiles(files, "js");
                AddSiteFiles(files, "fonts");
                AddSiteFiles(files, "images");
                AddSiteFiles(files, "pages");
                files.Add(ManifestFile.FromPath("js/daf/add.min.js"));
                files.Add(ManifestFile.FromPath("css/daf/add.min.css"));
                files.Add(ManifestFile.FromPath("touch-settings.json"));
                // add config.js
                var pageContent = GetConfigJS();
                response["config"] = JObject.FromObject(ManifestFile.GetConfig(pageContent));
                // build required js
                var combinedScript = AquariumExtenderBase.EnableCombinedScript;
                AquariumExtenderBase.EnableCombinedScript = true;
                foreach (var s in AquariumExtenderBase.StandardScripts(true))
                	if (!(string.IsNullOrEmpty(s.Path)))
                    	reqFiles.Add(ManifestFile.FromPath(s.Path));
                    else
                    {
                        var f = ManifestFile.FromResource(s.Name);
                        files.Add(f);
                        reqFiles.Add(f);
                    }
                AquariumExtenderBase.EnableCombinedScript = combinedScript;
                var require = new JObject();
                response["require"] = require;
                require["js"] = JArray.FromObject(reqFiles);
                reqFiles.Clear();
                // build required css
                foreach (var css in services.EnumerateTouchUIStylesheets())
                	if (!(Path.GetFileName(css).StartsWith("touch-theme")))
                    	reqFiles.Add(ManifestFile.FromPath(css));
                require["css"] = JArray.FromObject(reqFiles);
                // complete files
                response["files"] = JArray.FromObject(files);
            }
            return response;
        }
        
        public override object HandleException(JObject args, Exception ex)
        {
            throw ex;
        }
        
        void AddSiteFiles(List<ManifestFile> files, string siteFolder)
        {
            var rootPath = HttpContext.Current.Server.MapPath("~\\");
            foreach (var file in Directory.EnumerateFiles(Path.Combine(rootPath, siteFolder), "*.*", SearchOption.AllDirectories))
            {
                var m = Regex.Match(file, "\\.(?\'Culture\'\\w\\w(-\\w\\w)?)\\.(\\w+)$");
                if (!m.Success || (m.Groups["Culture"].Value == Thread.CurrentThread.CurrentCulture.Name))
                	if (siteFolder == "pages")
                    	files.Add(ManifestFile.FromUrl(file.Substring(rootPath.Length)));
                    else
                    	files.Add(ManifestFile.FromPath(file.Substring(rootPath.Length)));
            }
        }
        
        public static string GetConfigJS()
        {
            var m = Regex.Match(ManifestFile.GetContent(ApplicationServices.HomePageUrl), "<\\/footer><script >\\s*(?\'Config\'[\\S\\s]+?)<\\/script>");
            if (!m.Success)
            	throw new Exception("Home Page failed to process.");
            return m.Groups["Config"].Value;
        }
    }
    
    public class GetManifestFileServiceRequestHandler : ServiceRequestHandler
    {
        
        public override bool RequiresAuthentication
        {
            get
            {
                return true;
            }
        }
        
        public override object HandleRequest(DataControllerService service, JObject args)
        {
            var context = HttpContext.Current;
            var requestedFile = ((string)(args["file"]));
            if (string.IsNullOrEmpty(requestedFile))
            	throw new HttpException(400, "File argument is required.");
            requestedFile = requestedFile.Replace('/', '\\');
            if (requestedFile == "js\\host\\config.js")
            	context.Response.Write(GetManifestServiceRequestHandler.GetConfigJS());
            else
            	if (requestedFile.StartsWith("_resources\\"))
                {
                    var resourceName = requestedFile.Substring(11);
                    using (var s = ControllerConfigurationUtility.GetResourceStream(resourceName))
                    	if (s != null)
                        	s.CopyTo(context.Response.OutputStream);
                        else
                        	throw new HttpException(404, "File does not exist.");
                }
                else
                	if (requestedFile == "js\\daf\\add.min.js")
                    	context.Response.Write(ApplicationServices.Current.AddScripts());
                    else
                    	if (requestedFile == "css\\daf\\add.min.css")
                        	context.Response.Write(ApplicationServices.Current.AddStyleSheets());
                        else
                        	if (Path.GetExtension(requestedFile).StartsWith(".htm"))
                            	context.Response.Write(ManifestFile.GetContent(("~/" + requestedFile.Substring(0, requestedFile.IndexOf('.')).Replace('\\', '/'))));
                            else
                            {
                                var p = context.Server.MapPath(("~\\" + requestedFile));
                                if (File.Exists(p))
                                	using (var f = File.OpenRead(p))
                                    	f.CopyTo(context.Response.OutputStream);
                                else
                                	throw new HttpException(404, string.Format("File {0} does not exist.", p));
                            }
            context.Response.End();
            return null;
        }
        
        public override object HandleException(JObject args, Exception ex)
        {
            if (ex is ThreadAbortException)
            	return null;
            throw ex;
        }
    }
    
    public class ServiceRequestError
    {
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _exceptionType;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _message;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _stackTrace;
        
        public string ExceptionType
        {
            get
            {
                return this._exceptionType;
            }
            set
            {
                this._exceptionType = value;
            }
        }
        
        public string Message
        {
            get
            {
                return this._message;
            }
            set
            {
                this._message = value;
            }
        }
        
        public string StackTrace
        {
            get
            {
                return this._stackTrace;
            }
            set
            {
                this._stackTrace = value;
            }
        }
    }
    
    public class ServiceRequestRedirectException : Exception
    {
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _redirectUrl;
        
        public ServiceRequestRedirectException(string redirectUrl)
        {
            this.RedirectUrl = redirectUrl;
        }
        
        public virtual string RedirectUrl
        {
            get
            {
                return this._redirectUrl;
            }
            set
            {
                this._redirectUrl = value;
            }
        }
    }
    
    public partial class RequestValidationService : RequestValidationServiceBase
    {
    }
    
    public class RequestValidationServiceBase
    {
        
        public static Regex ValidRequestRegex = new Regex("<[^\\w<>]*(?:[^<>\"\'\\s]*:)?[^\\w<>]*(?:\\W*s\\W*c\\W*r\\W*i\\W*p\\W*t|\\W*f\\W*o\\W*r\\W*m|\\W*" +
                "s\\W*t\\W*y\\W*l\\W*e|\\W*s\\W*v\\W*g|\\W*m\\W*a\\W*r\\W*q\\W*u\\W*e\\W*e|(?:\\W*l\\W*i\\W*n\\W*k|" +
                "\\W*o\\W*b\\W*j\\W*e\\W*c\\W*t|\\W*e\\W*m\\W*b\\W*e\\W*d|\\W*a\\W*p\\W*p\\W*l\\W*e\\W*t|\\W*p\\W*a\\" +
                "W*r\\W*a\\W*m|\\W*i?\\W*f\\W*r\\W*a\\W*m\\W*e|\\W*b\\W*a\\W*s\\W*e|\\W*b\\W*o\\W*d\\W*y|\\W*m\\W*e" +
                "\\W*t\\W*a|\\W*i\\W*m\\W*a?\\W*g\\W*e?|\\W*v\\W*i\\W*d\\W*e\\W*o|\\W*a\\W*u\\W*d\\W*i\\W*o|\\W*b\\W" +
                "*i\\W*n\\W*d\\W*i\\W*n\\W*g\\W*s|\\W*s\\W*e\\W*t|\\W*i\\W*s\\W*i\\W*n\\W*d\\W*e\\W*x|\\W*a\\W*n\\W*" +
                "i\\W*m\\W*a\\W*t\\W*e)[^>\\w])|(?:<\\w[\\s\\S]*[\\s\\0\\/]|[\'\"])(?:formaction|style|backgro" +
                "und|src|lowsrc|ping|on(?:d(?:e(?:vice(?:(?:orienta|mo)tion|proximity|found|light" +
                ")|livery(?:success|error)|activate)|r(?:ag(?:e(?:n(?:ter|d)|xit)|(?:gestur|leav)" +
                "e|start|drop|over)?|op)|i(?:s(?:c(?:hargingtimechange|onnect(?:ing|ed))|abled)|a" +
                "ling)|ata(?:setc(?:omplete|hanged)|(?:availabl|chang)e|error)|urationchange|ownl" +
                "oading|blclick)|Moz(?:M(?:agnifyGesture(?:Update|Start)?|ouse(?:PixelScroll|Hitt" +
                "est))|S(?:wipeGesture(?:Update|Start|End)?|crolledAreaChanged)|(?:(?:Press)?TapG" +
                "estur|BeforeResiz)e|EdgeUI(?:C(?:omplet|ancel)|Start)ed|RotateGesture(?:Update|S" +
                "tart)?|A(?:udioAvailable|fterPaint))|c(?:o(?:m(?:p(?:osition(?:update|start|end)" +
                "|lete)|mand(?:update)?)|n(?:t(?:rolselect|extmenu)|nect(?:ing|ed))|py)|a(?:(?:ll" +
                "schang|ch)ed|nplay(?:through)?|rdstatechange)|h(?:(?:arging(?:time)?ch)?ange|eck" +
                "ing)|(?:fstate|ell)change|u(?:echange|t)|l(?:ick|ose))|m(?:o(?:z(?:pointerlock(?" +
                ":change|error)|(?:orientation|time)change|fullscreen(?:change|error)|network(?:d" +
                "own|up)load)|use(?:(?:lea|mo)ve|o(?:ver|ut)|enter|wheel|down|up)|ve(?:start|end)" +
                "?)|essage|ark)|s(?:t(?:a(?:t(?:uschanged|echange)|lled|rt)|k(?:sessione|comma)nd" +
                "|op)|e(?:ek(?:complete|ing|ed)|(?:lec(?:tstar)?)?t|n(?:ding|t))|u(?:ccess|spend|" +
                "bmit)|peech(?:start|end)|ound(?:start|end)|croll|how)|b(?:e(?:for(?:e(?:(?:scrip" +
                "texecu|activa)te|u(?:nload|pdate)|p(?:aste|rint)|c(?:opy|ut)|editfocus)|deactiva" +
                "te)|gin(?:Event)?)|oun(?:dary|ce)|l(?:ocked|ur)|roadcast|usy)|a(?:n(?:imation(?:" +
                "iteration|start|end)|tennastatechange)|fter(?:(?:scriptexecu|upda)te|print)|udio" +
                "(?:process|start|end)|d(?:apteradded|dtrack)|ctivate|lerting|bort)|DOM(?:Node(?:" +
                "Inserted(?:IntoDocument)?|Removed(?:FromDocument)?)|(?:CharacterData|Subtree)Mod" +
                "ified|A(?:ttrModified|ctivate)|Focus(?:Out|In)|MouseScroll)|r(?:e(?:s(?:u(?:m(?:" +
                "ing|e)|lt)|ize|et)|adystatechange|pea(?:tEven)?t|movetrack|trieving|ceived)|ow(?" +
                ":s(?:inserted|delete)|e(?:nter|xit))|atechange)|p(?:op(?:up(?:hid(?:den|ing)|sho" +
                "w(?:ing|n))|state)|a(?:ge(?:hide|show)|(?:st|us)e|int)|ro(?:pertychange|gress)|l" +
                "ay(?:ing)?)|t(?:ouch(?:(?:lea|mo)ve|en(?:ter|d)|cancel|start)|ime(?:update|out)|" +
                "ransitionend|ext)|u(?:s(?:erproximity|sdreceived)|p(?:gradeneeded|dateready)|n(?" +
                ":derflow|load))|f(?:o(?:rm(?:change|input)|cus(?:out|in)?)|i(?:lterchange|nish)|" +
                "ailed)|l(?:o(?:ad(?:e(?:d(?:meta)?data|nd)|start)?|secapture)|evelchange|y)|g(?:" +
                "amepad(?:(?:dis)?connected|button(?:down|up)|axismove)|et)|e(?:n(?:d(?:Event|ed)" +
                "?|abled|ter)|rror(?:update)?|mptied|xit)|i(?:cc(?:cardlockerror|infochange)|n(?:" +
                "coming|valid|put))|o(?:(?:(?:ff|n)lin|bsolet)e|verflow(?:changed)?|pen)|SVG(?:(?" +
                ":Unl|L)oad|Resize|Scroll|Abort|Error|Zoom)|h(?:e(?:adphoneschange|l[dp])|ashchan" +
                "ge|olding)|v(?:o(?:lum|ic)e|ersion)change|w(?:a(?:it|rn)ing|heel)|key(?:press|do" +
                "wn|up)|(?:AppComman|Loa)d|no(?:update|match)|Request|zoom))[\\s\\0]*=");
        
        public static JObject ToJson(HttpContext context)
        {
            var service = new RequestValidationService();
            var data = new byte[context.Request.InputStream.Length];
            context.Request.InputStream.Read(data, 0, data.Length);
            var args = service.ValidateJson(Encoding.UTF8.GetString(data), context);
            JObject json = null;
            if (!(string.IsNullOrEmpty(args)))
            	json = service.ValidateJson(JObject.Parse(args), context);
            return json;
        }
        
        public virtual string ValidateJson(string json, HttpContext context)
        {
            if (ValidRequestRegex.IsMatch(json))
            	throw new HttpException(400, "Bad Request");
            return HttpUtility.HtmlDecode(json);
        }
        
        public virtual JObject ValidateJson(JObject json, HttpContext context)
        {
            var isBad = false;
            if (json["IgnoreBusinessRules"] != null)
            	isBad = true;
            if (json["requests"] != null)
            {
                var list = ((JArray)(json["requests"]));
                foreach (var args in list.Values<JObject>())
                	if (args["IgnoreBusinessRules"] != null)
                    {
                        isBad = true;
                        break;
                    }
            }
            if (isBad)
            	throw new HttpException(400, "Bad Request");
            return json;
        }
    }
    
    public class WorkflowResources
    {
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private SortedDictionary<string, string> _staticResources;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private List<Regex> _dynamicResources;
        
        public WorkflowResources()
        {
            _staticResources = new SortedDictionary<string, string>();
            _dynamicResources = new List<Regex>();
        }
        
        public SortedDictionary<string, string> StaticResources
        {
            get
            {
                return this._staticResources;
            }
            set
            {
                this._staticResources = value;
            }
        }
        
        public List<Regex> DynamicResources
        {
            get
            {
                return this._dynamicResources;
            }
            set
            {
                this._dynamicResources = value;
            }
        }
    }
    
    public partial class WorkflowRegister : WorkflowRegisterBase
    {
    }
    
    public class WorkflowRegisterBase
    {
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private SortedDictionary<string, WorkflowResources> _resources;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private SortedDictionary<string, List<string>> _roleRegister;
        
        public WorkflowRegisterBase()
        {
            // initialize system workflows
            _resources = new SortedDictionary<string, WorkflowResources>();
            RegisterBuiltinWorkflowResources();
            foreach (var w in ApplicationServices.Current.ReadSiteContent("sys/workflows%", "%"))
            {
                var text = w.Text;
                if (!(string.IsNullOrEmpty(text)))
                {
                    WorkflowResources wr = null;
                    if (!(Resources.TryGetValue(w.PhysicalName, out wr)))
                    {
                        wr = new WorkflowResources();
                        Resources[w.PhysicalName] = wr;
                    }
                    foreach (var s in text.Split(new char[] {
                                '\n'}, StringSplitOptions.RemoveEmptyEntries))
                    {
                        var query = s.Trim();
                        if (!(string.IsNullOrEmpty(query)))
                        	if (s.StartsWith("regex "))
                            {
                                var regexQuery = s.Substring(6).Trim();
                                if (!(string.IsNullOrEmpty(regexQuery)))
                                	try
                                    {
                                        wr.DynamicResources.Add(new Regex(regexQuery, RegexOptions.IgnoreCase));
                                    }
                                    catch (Exception)
                                    {
                                    }
                            }
                            else
                            	wr.StaticResources[query.ToLower()] = query;
                    }
                }
            }
            // read "role" workflows from the register
            _roleRegister = new SortedDictionary<string, List<string>>();
            foreach (var rr in ApplicationServices.Current.ReadSiteContent("sys/register/roles%", "%"))
            {
                var text = rr.Text;
                if (!(string.IsNullOrEmpty(text)))
                {
                    List<string> workflows = null;
                    if (!(RoleRegister.TryGetValue(rr.PhysicalName, out workflows)))
                    {
                        workflows = new List<string>();
                        RoleRegister[rr.PhysicalName] = workflows;
                    }
                    foreach (var s in text.Split(new char[] {
                                '\n',
                                ','}, StringSplitOptions.RemoveEmptyEntries))
                    {
                        var name = s.Trim();
                        if (!(string.IsNullOrEmpty(name)))
                        	workflows.Add(name);
                    }
                }
            }
        }
        
        public SortedDictionary<string, WorkflowResources> Resources
        {
            get
            {
                return this._resources;
            }
            set
            {
                this._resources = value;
            }
        }
        
        public SortedDictionary<string, List<string>> RoleRegister
        {
            get
            {
                return this._roleRegister;
            }
            set
            {
                this._roleRegister = value;
            }
        }
        
        public List<string> UserWorkflows
        {
            get
            {
                var workflows = ((List<string>)(HttpContext.Current.Items["WorkflowRegister_UserWorkflows"]));
                if (workflows == null)
                {
                    workflows = new List<string>();
                    var identity = HttpContext.Current.User.Identity;
                    if (identity.IsAuthenticated)
                    	foreach (var urf in ApplicationServices.Current.ReadSiteContent("sys/register/users%", identity.Name))
                        {
                            var text = urf.Text;
                            if (!(string.IsNullOrEmpty(text)))
                            	foreach (var s in text.Split(new char[] {
                                            '\n',
                                            ','}, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    var name = s.Trim();
                                    if (!(string.IsNullOrEmpty(name)) && !(workflows.Contains(name)))
                                    	workflows.Add(name);
                                }
                        }
                    // enumerate role workflows
                    var isAuthenticated = HttpContext.Current.User.Identity.IsAuthenticated;
                    foreach (var role in RoleRegister.Keys)
                    	if ((((role == "?") && !isAuthenticated) || ((role == "*") && isAuthenticated)) || DataControllerBase.UserIsInRole(role))
                        	foreach (var name in RoleRegister[role])
                            	if (!(workflows.Contains(name)))
                                	workflows.Add(name);
                    HttpContext.Current.Items["WorkflowRegister_UserWorkflows"] = workflows;
                }
                return workflows;
            }
        }
        
        public bool Enabled
        {
            get
            {
                return (_resources.Count > 0);
            }
        }
        
        public static bool IsEnabled
        {
            get
            {
                if (!ApplicationServices.IsSiteContentEnabled)
                	return false;
                var wr = WorkflowRegister.GetCurrent();
                return ((wr != null) && wr.Enabled);
            }
        }
        
        public virtual int CacheDuration
        {
            get
            {
                return 30;
            }
        }
        
        protected virtual void RegisterBuiltinWorkflowResources()
        {
        }
        
        public static bool Allows(string fileName)
        {
            if (!ApplicationServices.IsSiteContentEnabled)
            	return false;
            var wr = WorkflowRegister.GetCurrent(fileName);
            if ((wr == null) || !wr.Enabled)
            	return false;
            return wr.IsMatch(fileName);
        }
        
        public bool IsMatch(string physicalPath, string physicalName)
        {
            var fileName = physicalPath;
            if (string.IsNullOrEmpty(fileName))
            	fileName = physicalName;
            else
            	fileName = ((fileName + "/") 
                            + physicalName);
            return IsMatch(fileName);
        }
        
        public bool IsMatch(string fileName)
        {
            fileName = fileName.ToLower();
            var activeWorkflows = UserWorkflows;
            foreach (var wf in activeWorkflows)
            {
                WorkflowResources resourceList = null;
                if (Resources.TryGetValue(wf, out resourceList))
                {
                    if (resourceList.StaticResources.ContainsKey(fileName))
                    	return true;
                    foreach (var re in resourceList.DynamicResources)
                    	if (re.IsMatch(fileName))
                        	return true;
                }
            }
            return false;
        }
        
        public static WorkflowRegister GetCurrent()
        {
            return GetCurrent(null);
        }
        
        public static WorkflowRegister GetCurrent(string relativePath)
        {
            if (!(ApplicationServicesBase.Create().Supports(ApplicationFeature.WorkflowRegister)))
            	return null;
            if ((relativePath != null) && (relativePath.StartsWith("sys/workflows") || relativePath.StartsWith("sys/register")))
            	return null;
            var key = "WorkflowRegister_Current";
            var context = HttpContext.Current;
            var instance = ((WorkflowRegister)(context.Items[key]));
            if (instance == null)
            {
                instance = ((WorkflowRegister)(context.Cache[key]));
                if (instance == null)
                {
                    instance = new WorkflowRegister();
                    context.Cache.Add(key, instance, null, DateTime.Now.AddSeconds(instance.CacheDuration), Cache.NoSlidingExpiration, CacheItemPriority.AboveNormal, null);
                }
                context.Items[key] = instance;
            }
            return instance;
        }
    }
    
    public enum SiteContentFields
    {
        
        SiteContentId,
        
        DataFileName,
        
        DataContentType,
        
        Length,
        
        Path,
        
        Data,
        
        Roles,
        
        Users,
        
        Text,
        
        CacheProfile,
        
        RoleExceptions,
        
        UserExceptions,
        
        Schedule,
        
        ScheduleExceptions,
    }
    
    public class SiteContentFile
    {
        
        private object _id;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _name;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _path;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _contentType;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int _length;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private byte[] _data;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _physicalName;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _error;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _schedule;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _scheduleExceptions;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _cacheProfile;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int _cacheDuration;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private HttpCacheability _cacheLocation;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string[] _cacheVaryByParams;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string[] _cacheVaryByHeaders;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool _cacheNoStore;
        
        public SiteContentFile()
        {
            this.CacheLocation = HttpCacheability.NoCache;
        }
        
        public object Id
        {
            get
            {
                return _id;
            }
            set
            {
                if ((value != null) && (value.GetType() == typeof(byte[])))
                	value = new Guid(((byte[])(value)));
                _id = value;
            }
        }
        
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
        
        public string Path
        {
            get
            {
                return this._path;
            }
            set
            {
                this._path = value;
            }
        }
        
        public string ContentType
        {
            get
            {
                return this._contentType;
            }
            set
            {
                this._contentType = value;
            }
        }
        
        public int Length
        {
            get
            {
                return this._length;
            }
            set
            {
                this._length = value;
            }
        }
        
        public byte[] Data
        {
            get
            {
                return this._data;
            }
            set
            {
                this._data = value;
            }
        }
        
        public string PhysicalName
        {
            get
            {
                return this._physicalName;
            }
            set
            {
                this._physicalName = value;
            }
        }
        
        public string FullName
        {
            get
            {
                return (Path 
                            + ("/" + PhysicalName));
            }
        }
        
        public string Error
        {
            get
            {
                return this._error;
            }
            set
            {
                this._error = value;
            }
        }
        
        public string Schedule
        {
            get
            {
                return this._schedule;
            }
            set
            {
                this._schedule = value;
            }
        }
        
        public string ScheduleExceptions
        {
            get
            {
                return this._scheduleExceptions;
            }
            set
            {
                this._scheduleExceptions = value;
            }
        }
        
        public string CacheProfile
        {
            get
            {
                return this._cacheProfile;
            }
            set
            {
                this._cacheProfile = value;
            }
        }
        
        public int CacheDuration
        {
            get
            {
                return this._cacheDuration;
            }
            set
            {
                this._cacheDuration = value;
            }
        }
        
        public HttpCacheability CacheLocation
        {
            get
            {
                return this._cacheLocation;
            }
            set
            {
                this._cacheLocation = value;
            }
        }
        
        public string[] CacheVaryByParams
        {
            get
            {
                return this._cacheVaryByParams;
            }
            set
            {
                this._cacheVaryByParams = value;
            }
        }
        
        public string[] CacheVaryByHeaders
        {
            get
            {
                return this._cacheVaryByHeaders;
            }
            set
            {
                this._cacheVaryByHeaders = value;
            }
        }
        
        public bool CacheNoStore
        {
            get
            {
                return this._cacheNoStore;
            }
            set
            {
                this._cacheNoStore = value;
            }
        }
        
        public string Text
        {
            get
            {
                if ((this.Data != null) && IsText)
                	return Encoding.UTF8.GetString(this.Data);
                return null;
            }
            set
            {
                if (value == null)
                	_data = null;
                else
                {
                    _data = Encoding.UTF8.GetBytes(value);
                    _contentType = "text/plain";
                }
            }
        }
        
        public bool IsText
        {
            get
            {
                return ((_contentType != null) && Regex.IsMatch(_contentType, "^((text/\\w+)|(application/(javascript|json)))$"));
            }
        }
        
        public static byte[] ReadAllBytes(string relativePath)
        {
            return ApplicationServices.Current.ReadSiteContentBytes(relativePath);
        }
        
        public static int WriteAllBytes(string relativePath, byte[] data)
        {
            return WriteAllBytes(relativePath, MimeMapping.GetMimeMapping(System.IO.Path.GetFileName(relativePath)), data);
        }
        
        public static int WriteAllBytes(string relativePath, string contentType, byte[] data)
        {
            var services = ApplicationServices.Current;
            var values = ToValues(relativePath, contentType, true);
            values.Add(new FieldValue(services.SiteContentFieldName(SiteContentFields.Data), data));
            values.Add(new FieldValue(services.SiteContentFieldName(SiteContentFields.Length), null));
            if (data != null)
            {
                values.Last().NewValue = data.Length;
                values.Last().Modified = true;
            }
            return Write(values).RowsAffected;
        }
        
        public static string ReadAllText(string relativePath)
        {
            return ApplicationServices.Current.ReadSiteContentString(relativePath);
        }
        
        public static JObject ReadJson(string relativePath)
        {
            var result = ReadAllText(relativePath);
            if (!(string.IsNullOrEmpty(result)) && (result[0] == '{'))
            	return JObject.Parse(result);
            return new JObject();
        }
        
        public static int WriteAllText(string relativePath, string text)
        {
            return WriteAllText(relativePath, "text/plain", text);
        }
        
        public static int WriteAllText(string relativePath, string contentType, string text)
        {
            var values = ToValues(relativePath, contentType, true);
            values.Add(new FieldValue(ApplicationServices.Current.SiteContentFieldName(SiteContentFields.Text), text));
            return Write(values).RowsAffected;
        }
        
        public static int WriteJson(string relativePath, JObject json)
        {
            return WriteAllText(relativePath, "application/json", json.ToString());
        }
        
        public static ActionResult Write(List<FieldValue> values)
        {
            // find Data, FileName, and ContentType field values
            var dataFieldName = ApplicationServices.Current.SiteContentFieldName(SiteContentFields.Data);
            var fileNameFieldName = ApplicationServices.Current.SiteContentFieldName(SiteContentFields.DataFileName);
            var contentTypeFieldName = ApplicationServices.Current.SiteContentFieldName(SiteContentFields.DataContentType);
            FieldValue dataFieldValue = null;
            FieldValue fileNameFieldValue = null;
            FieldValue contentTypeFieldValue = null;
            foreach (var fvo in values)
            {
                if (fvo.Name == dataFieldName)
                	dataFieldValue = fvo;
                if (fvo.Name == fileNameFieldName)
                	fileNameFieldValue = fvo;
                if (fvo.Name == contentTypeFieldName)
                	contentTypeFieldValue = fvo;
            }
            // remove "Data" field from the values. We will use Blob.Write to persist the data
            if (dataFieldValue != null)
            	values.Remove(dataFieldValue);
            //  Insert or Update the record
            var args = new ActionArgs();
            args.Controller = ApplicationServices.Current.GetSiteContentControllerName();
            args.View = "createForm1";
            args.Values = values.ToArray();
            args.LastCommandName = "New";
            args.CommandName = "Insert";
            args.IgnoreBusinessRules = true;
            if (values[0].OldValue != null)
            {
                args.View = "editForm1";
                args.LastCommandName = null;
                args.CommandName = "Update";
            }
            ActionResult result = null;
            var access = Controller.GrantFullAccess("SiteContent");
            try
            {
                var c = ControllerFactory.CreateDataController();
                result = c.Execute(args.Controller, args.View, args);
                result.RaiseExceptionIfErrors();
                //  If there is Data field, then write it with Blob.Write instead. This will ensure that adapters are correctly engaged.
                if (dataFieldValue != null)
                {
                    var dataField = ((DataControllerBase)(c)).CreateViewPage().FindField(dataFieldName);
                    var blobHandler = dataField.OnDemandHandler;
                    var blobKey = values[0].Value;
                    Blob.Write(blobHandler, blobKey, fileNameFieldValue.Value.ToString(), contentTypeFieldValue.Value.ToString(), ((byte[])(dataFieldValue.Value)));
                }
            }
            finally
            {
                Controller.RevokeFullAccess(access);
            }
            return result;
        }
        
        public static int Delete(string relativePath)
        {
            var services = ApplicationServices.Current;
            var values = ToValues(relativePath, null, false);
            var keys = new List<string>();
            foreach (var file in services.ReadSiteContent(((string)(values[2].Value)), ((string)(values[1].Value))))
            	keys.Add(file.Id.ToString());
            if (keys.Count > 0)
            {
                var args = new ActionArgs();
                args.Controller = services.GetSiteContentControllerName();
                args.View = "grid1";
                args.Values = new FieldValue[] {
                        new FieldValue(values[0].Name, keys[0], keys[0])};
                args.SelectedValues = keys.ToArray();
                args.CommandName = "Delete";
                args.IgnoreBusinessRules = true;
                var access = Controller.GrantFullAccess("SiteContent");
                try
                {
                    var c = ControllerFactory.CreateDataController();
                    var result = c.Execute(args.Controller, args.View, args);
                    result.RaiseExceptionIfErrors();
                    return result.RowsAffected;
                }
                finally
                {
                    Controller.RevokeFullAccess(access);
                }
            }
            return 0;
        }
        
        public static bool Exists(string relativePath)
        {
            return (ApplicationServices.Current.ReadSiteContent(relativePath).Length > 0);
        }
        
        private static List<FieldValue> ToValues(string relativePath, string contentType, bool checkForExisting)
        {
            var services = ApplicationServices.Current;
            var name = relativePath;
            string path = null;
            var index = relativePath.LastIndexOf("/");
            if (index >= 0)
            {
                name = relativePath.Substring((index + 1));
                path = relativePath.Substring(0, index);
            }
            var list = new List<FieldValue>();
            list.Add(new FieldValue(services.SiteContentFieldName(SiteContentFields.SiteContentId)));
            list.Add(new FieldValue(services.SiteContentFieldName(SiteContentFields.DataFileName), name));
            list.Add(new FieldValue(services.SiteContentFieldName(SiteContentFields.Path), path));
            list.Add(new FieldValue(services.SiteContentFieldName(SiteContentFields.DataContentType)));
            if (checkForExisting)
            {
                var file = services.ReadSiteContent(relativePath);
                if (file != null)
                {
                    list[0].OldValue = file.Id;
                    list[0].Modified = false;
                    list[1].OldValue = file.Name;
                    list[2].OldValue = file.Path;
                    list[3].OldValue = file.ContentType;
                }
            }
            if (!(string.IsNullOrEmpty(contentType)))
            {
                list[3].NewValue = contentType;
                list[3].Modified = true;
            }
            return list;
        }
        
        public override string ToString()
        {
            return string.Format("{0}/{1}", Path, Name);
        }
    }
    
    public class SiteContentFileList : List<SiteContentFile>
    {
    }
    
    public partial class ApplicationServices : EnterpriseApplicationServices
    {
        
        public static string CustomCodeAssemblyName = null;
        
        public static string CombinedResourceType
        {
            get
            {
                var t = string.Empty;
                if (!AuthorizationIsSupported)
                	t = (t + "_noauth");
                var addonFile = Regex.Match(HttpContext.Current.Request.Url.LocalPath, "^\\/(_\\w+)");
                if (addonFile.Success)
                	t = (t + addonFile.Groups[1].Value);
                return t;
            }
        }
        
        public static string HomePageUrl
        {
            get
            {
                return Create().UserHomePageUrl();
            }
        }
        
        public static System.Type StringToType(string typeName)
        {
            var qualifiedTypeName = typeName;
            if (!(string.IsNullOrEmpty(CustomCodeAssemblyName)))
            	qualifiedTypeName = (qualifiedTypeName 
                            + ("," + CustomCodeAssemblyName));
            var t = Type.GetType(qualifiedTypeName);
            if (t == null)
            	t = Type.GetType(typeName);
            return t;
        }
        
        public static object CreateInstance(string typeName)
        {
            return Activator.CreateInstance(StringToType(typeName));
        }
        
        public static void Initialize()
        {
            string appFrameworkConfigTypeName = null;
            // figure the name of the custom code assembly
            var customCodeAssembly = typeof(ApplicationServices).Assembly;
            if (customCodeAssembly.GetName().Name == "FreeTrial")
            	foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
                	if (a.FullName.StartsWith("App_Code"))
                    {
                        customCodeAssembly = a;
                        break;
                    }
            CustomCodeAssemblyName = customCodeAssembly.FullName;
            // find the full name of AppFrameworkConfig class
            foreach (var t in customCodeAssembly.GetTypes())
            	if (t.Name == "AppFrameworkConfig")
                {
                    appFrameworkConfigTypeName = t.FullName;
                    break;
                }
            // initialize external components of the framework
            var frameworkConfig = CreateInstance(appFrameworkConfigTypeName);
            if (frameworkConfig != null)
            	frameworkConfig.GetType().InvokeMember("Initialize", (BindingFlags.InvokeMethod | (BindingFlags.Instance | BindingFlags.Public)), null, frameworkConfig, null);
            foreach (var className in new string[] {
                    "AppBuilder",
                    "FormBuilder",
                    "OfflineSync",
                    "Survey"})
            	try
                {
                    var addonType = Type.GetType(string.Format("CodeOnTime.Addons.{0},addon.{0}", className));
                    if (addonType != null)
                    	Addons.Add(Activator.CreateInstance(addonType));
                }
                catch (Exception)
                {
                }
            // register service routes and map handlers
            Create().RegisterServices();
        }
        
        public static object Login(string username, string password, bool createPersistentCookie)
        {
            return Create().AuthenticateUser(username, password, createPersistentCookie);
        }
        
        public static void Logout()
        {
            Create().UserLogout();
        }
        
        public static string[] Roles()
        {
            return Create().UserRoles();
        }
        
        public static JObject Themes()
        {
            return Create().UserThemes();
        }
        
        public static string OAuthGetAuthorizationUrl(string provider)
        {
            return OAuthGetAuthorizationUrl(provider, null);
        }
        
        public static string OAuthGetAuthorizationUrl(string provider, string state)
        {
            string authorizationUrl = null;
            Type oauthHandlerType = null;
            if (OAuthHandlerFactory.Handlers.TryGetValue(provider.ToLower(), out oauthHandlerType))
            {
                var handler = ((OAuthHandler)(Activator.CreateInstance(oauthHandlerType)));
                handler.StartPage = Create().UserHomePageUrl();
                handler.AppState = state;
                authorizationUrl = handler.GetAuthorizationUrl();
            }
            return authorizationUrl;
        }
    }
    
    public class AddonRouteIgnoreConstraint
    {
        
        public string PathInfo
        {
            get
            {
                return "^(?!daf\\/add\\.min\\.(js|css)$).+";
            }
        }
    }
    
    public enum ApplicationFeature
    {
        
        DynamicAccessControlList,
        
        DynamicControllerCustomization,
        
        WorkflowRegister,
    }
    
    public class ApplicationServicesBase
    {
        
        public static List<object> Addons = new List<object>();
        
        public static bool EnableMobileClient = true;
        
        private JObject _defaultSettings;
        
        private static bool _enableCombinedCss;
        
        private static bool _enableMinifiedCss = true;
        
        public static string FrameworkSiteContentControllerName = string.Empty;
        
        public static Regex NameValueListRegex = new Regex("^\\s*(?\'Name\'\\w+)\\s*=\\s*(?\'Value\'[\\S\\s]+?)\\s*$", RegexOptions.Multiline);
        
        public static Regex SystemResourceRegex = new Regex("~/((sys/)|(views/)|(controllers/)|(permissions/)|(reports/)|((site|touch\\-setting" +
                "s|acl)\\.\\w+))", RegexOptions.IgnoreCase);
        
        public static string FrameworkAppName = null;
        
        public static bool AuthorizationIsSupported = true;
        
        private string _userTheme;
        
        private string _userAccent;
        
        public static Regex CssUrlRegex = new Regex("(?\'Header\'\\burl\\s*\\(\\s*(\\\"|\\\')?)(?\'Name\'[\\w/\\.]+)(?\'Symbol\'\\S)");
        
        public static Regex DefaultExcludeScriptRegex = new Regex("^(daf\\\\|sys\\\\|lib\\\\|surveys\\\\|_references\\.js)|((.+?)\\.(\\w\\w(\\-\\w+)*)\\.js$)");
        
        public static SortedDictionary<string, ServiceRequestHandler> RequestHandlers = new SortedDictionary<string, ServiceRequestHandler>();
        
        public static Regex ViewPageCompressRegex = new Regex("((\"(DefaultValue)\"\\:(\"[\\s\\S]*?\"))|(\"(Items|Pivots|Fields|Views|ActionGroups|Categ" +
                "ories|Filter|Expressions|Errors)\"\\:(\\[\\]))|(\"(Len|CategoryIndex|Rows|Columns|Sea" +
                "rch|ItemsPageSize|Aggregate|OnDemandStyle|TextMode|MaskType|AutoCompletePrefixLe" +
                "ngth|DataViewPageSize|PageOffset)\"\\:(0))|(\"(CausesValidation|AllowQBE|AllowSorti" +
                "ng|FormatOnClient|HtmlEncode|RequiresMetaData|RequiresRowCount|ShowInSelector|Da" +
                "taViewShow(ActionBar|Description|ViewSelector|PageSize|SearchBar|QuickFind))\"\\:(" +
                "true))|(\"(IsPrimaryKey|ReadOnly|HasDefaultValue|Hidden|AllowLEV|AllowNulls|OnDem" +
                "and|IsMirror|Calculated|CausesCalculate|IsVirtual|AutoSelect|SearchOnStart|ShowI" +
                "nSummary|ItemsLetters|WhenKeySelected|RequiresSiteContentText|RequiresPivot|Requ" +
                "iresAggregates|Floating|Collapsed|Label|SupportsCaching|AllowDistinctFieldInFilt" +
                "er|Flat|RequiresMetaData|RequiresRowCount|Distinct|(DataView(ShowInSummary|Multi" +
                "Select|ShowModalForms|SearchByFirstLetter|SearchOnStart|ShowRowNumber|AutoHighli" +
                "ghtFirstRow|AutoSelectFirstRow)))\"\\:(false))|(\"(AliasName|Tag|FooterText|ToolTip" +
                "|Watermark|DataFormatString|Copy|HyperlinkFormatString|SourceFields|SearchOption" +
                "s|ItemsDataController|ItemsTargetController|ItemsDataView|ItemsDataValueField|It" +
                "emsDataTextField|ItemsStyle|ItemsNewDataView|OnDemandHandler|Mask|ContextFields|" +
                "Formula|Flow|Label|Configuration|Editor|ItemsDescription|Group|CommandName|Comma" +
                "ndArgument|HeaderText|Description|CssClass|Confirmation|Notify|Key|WhenLastComma" +
                "ndName|WhenLastCommandArgument|WhenClientScript|WhenTag|WhenHRef|WhenView|PivotD" +
                "efinitions|Aggregates|PivotDefinitions|Aggregates|ViewType|LastView|StatusBar|Ic" +
                "ons|LEVs|QuickFindHint|InnerJoinPrimaryKey|SystemFilter|DistinctValueFieldName|C" +
                "lientScript|FirstLetters|SortExpression|Template|Tab|Wizard|InnerJoinForeignKey|" +
                "Expressions|ViewHeaderText|ViewLayout|GroupExpression|FieldFilter|Wrap|Tags|Tag|" +
                "Id|Filter|(DataView(Id|FilterSource|Controller|FilterFields|ShowActionButtons|Sh" +
                "owPager)))\"\\:(\"\\s*\"|null))|(\"Type\":\"String\")),?");
        
        public static Regex ViewPageCompress2Regex = new Regex(",\\}(,|])");
        
        public virtual JObject DefaultSettings
        {
            get
            {
                if (_defaultSettings == null)
                {
                    _defaultSettings = ((JObject)(HttpContext.Current.Items["touch-settings.json"]));
                    if (_defaultSettings == null)
                    {
                        _defaultSettings = ((JObject)(HttpContext.Current.Cache["touch-settings.json"]));
                        if (_defaultSettings == null)
                        {
                            var json = "{}";
                            var filePath = HttpContext.Current.Server.MapPath("~/touch-settings.json");
                            if (File.Exists(filePath))
                            	json = File.ReadAllText(filePath);
                            _defaultSettings = JObject.Parse(json);
                            EnsureJsonProperty(_defaultSettings, "appName", ApplicationServices.Current.Name);
                            EnsureJsonProperty(_defaultSettings, "map.apiKey", MapsApiIdentifier);
                            EnsureJsonProperty(_defaultSettings, "charts.maxPivotRowCount", MaxPivotRowCount);
                            EnsureJsonProperty(_defaultSettings, "ui.theme.name", "Light");
                            var ui = ((JObject)(_defaultSettings["ui"]));
                            EnsureJsonProperty(ui, "theme.accent", "Vantage");
                            EnsureJsonProperty(ui, "displayDensity.mobile", "Auto");
                            EnsureJsonProperty(ui, "displayDensity.desktop", "Tiny");
                            EnsureJsonProperty(ui, "list.labels.display", "DisplayedBelow");
                            EnsureJsonProperty(ui, "list.initialMode", "SeeAll");
                            EnsureJsonProperty(ui, "menu.location", "toolbar");
                            EnsureJsonProperty(ui, "actions.promote", true);
                            EnsureJsonProperty(ui, "smartDates", false);
                            EnsureJsonProperty(ui, "transitions.style", "");
                            EnsureJsonProperty(ui, "sidebar.when", "Landscape");
                            EnsureJsonProperty(_defaultSettings, "help.enabled", true);
                            HttpContext.Current.Cache.Add("touch-settings.json", _defaultSettings, new CacheDependency(new string[] {
                                            filePath,
                                            HttpContext.Current.Server.MapPath("~/web.config")}), Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                        }
                        HttpContext.Current.Items["touch-settings.json"] = _defaultSettings;
                    }
                }
                return _defaultSettings;
            }
        }
        
        public static bool EnableCombinedCss
        {
            get
            {
                return _enableCombinedCss;
            }
            set
            {
                _enableCombinedCss = value;
            }
        }
        
        public static bool EnableMinifiedCss
        {
            get
            {
                return _enableMinifiedCss;
            }
            set
            {
                _enableMinifiedCss = value;
            }
        }
        
        public static bool IsSiteContentEnabled
        {
            get
            {
                return !(string.IsNullOrEmpty(SiteContentControllerName));
            }
        }
        
        public static string SiteContentControllerName
        {
            get
            {
                return Create().GetSiteContentControllerName();
            }
        }
        
        public static string[] SiteContentEditors
        {
            get
            {
                return Create().GetSiteContentEditors();
            }
        }
        
        public static string[] SiteContentDevelopers
        {
            get
            {
                return Create().GetSiteContentDevelopers();
            }
        }
        
        public static string[] SuperUsers
        {
            get
            {
                return Create().GetSuperUsers();
            }
        }
        
        public static bool IsContentEditor
        {
            get
            {
                foreach (var r in Create().GetSiteContentEditors())
                	if (DataControllerBase.UserIsInRole(r))
                    	return true;
                return false;
            }
        }
        
        public static bool IsDeveloper
        {
            get
            {
                foreach (var r in Create().GetSiteContentDevelopers())
                	if (DataControllerBase.UserIsInRole(r))
                    	return true;
                return false;
            }
        }
        
        public static bool IsSuperUser
        {
            get
            {
                foreach (var r in Create().GetSuperUsers())
                	if (DataControllerBase.UserIsInRole(r))
                    	return true;
                return false;
            }
        }
        
        public static bool IsSafeMode
        {
            get
            {
                var request = HttpContext.Current.Request;
                var test = request.UrlReferrer;
                if (test == null)
                	test = request.Url;
                return ((test == null) && (test.ToString().Contains("_safemode=true") && DataControllerBase.UserIsInRole(SiteContentDevelopers)));
            }
        }
        
        public virtual int ScheduleCacheDuration
        {
            get
            {
                return 20;
            }
        }
        
        public virtual string Realm
        {
            get
            {
                return Name;
            }
        }
        
        public virtual string Name
        {
            get
            {
                return FrameworkAppName;
            }
        }
        
        public static string MapsApiIdentifier
        {
            get
            {
                if ((HttpContext.Current != null) && (HttpContext.Current.Request.Headers["X-Cot-Manifest-Request"] == "true"))
                	return WebConfigurationManager.AppSettings["MapsApiIdentifierMobile"];
                return WebConfigurationManager.AppSettings["MapsApiIdentifier"];
            }
        }
        
        public virtual int MaxPivotRowCount
        {
            get
            {
                return 250000;
            }
        }
        
        public static ApplicationServices Current
        {
            get
            {
                return Create();
            }
        }
        
        public static bool IsTouchClient
        {
            get
            {
                return true;
            }
        }
        
        public virtual string UserTheme
        {
            get
            {
                if (string.IsNullOrEmpty(_userTheme))
                	LoadTheme();
                return _userTheme;
            }
        }
        
        public virtual string UserAccent
        {
            get
            {
                if (string.IsNullOrEmpty(_userAccent))
                	LoadTheme();
                return _userAccent;
            }
        }
        
        public virtual bool EnableCors
        {
            get
            {
                return true;
            }
        }
        
        public virtual bool Supports(ApplicationFeature feature)
        {
            return false;
        }
        
        public static JToken Settings(string selector)
        {
            return SelectFrom(Current.DefaultSettings, selector);
        }
        
        public static JToken SelectFrom(JToken json, string selector)
        {
            var path = Regex.Split(selector, "\\.");
            for (var i = 0; (i < path.Length); i++)
            {
                json = json[path[i]];
                if (json == null)
                	break;
            }
            return json;
        }
        
        public virtual string GetNavigateUrl()
        {
            return null;
        }
        
        public static void VerifyUrl()
        {
            var navigateUrl = Create().GetNavigateUrl();
            if (!(string.IsNullOrEmpty(navigateUrl)))
            {
                var current = HttpContext.Current;
                if (!(VirtualPathUtility.ToAbsolute(navigateUrl).Equals(current.Request.RawUrl, StringComparison.CurrentCultureIgnoreCase)))
                	current.Response.Redirect(navigateUrl);
            }
        }
        
        public virtual void RegisterServices()
        {
            CreateStandardMembershipAccounts();
            var routes = RouteTable.Routes;
            RegisterIgnoredRoutes(routes);
            RegisterContentServices(routes);
            // Register service request handlers
            RequestHandlers.Add("getpage", new GetPageServiceRequestHandler());
            RequestHandlers.Add("getpagelist", new GetPageListServiceRequestHandler());
            RequestHandlers.Add("getlistofvalues", new GetListOfValuesServiceRequestHandler());
            RequestHandlers.Add("execute", new ExecuteServiceRequestHandler());
            RequestHandlers.Add("executeandgetpage", new ExecuteAndGetPageServiceRequestHandler());
            RequestHandlers.Add("executelist", new ExecuteListServiceRequestHandler());
            RequestHandlers.Add("getcompletionlist", new GetCompletionListServiceRequestHandler());
            RequestHandlers.Add("login", new LoginServiceRequestHandler());
            RequestHandlers.Add("logout", new LogoutServiceRequestHandler());
            RequestHandlers.Add("roles", new RolesServiceRequestHandler());
            RequestHandlers.Add("themes", new ThemesServiceRequestHandler());
            RequestHandlers.Add("savepermalink", new SavePermalinkServiceRequestHandler());
            RequestHandlers.Add("encodepermalink", new EncodePermalinkServiceRequestHandler());
            RequestHandlers.Add("listallpermalinks", new ListAllPermalinksServiceRequestHandler());
            RequestHandlers.Add("getsurvey", new GetSurveyServiceRequestHandler());
            RequestHandlers.Add("addon", new AddonServiceRequestHandler());
            OAuthHandlerFactory.Handlers.Add("dnn", typeof(DnnOAuthHandler));
            OAuthHandlerFactory.Handlers.Add("cloudidentity", typeof(CloudIdentityOAuthHandler));
            RequestHandlers.Add("getidentity", new GetIdentityServiceRequestHandler());
            RequestHandlers.Add("getmanifest", new GetManifestServiceRequestHandler());
            RequestHandlers.Add("getmanifestfile", new GetManifestFileServiceRequestHandler());
            RequestHandlers.Add("getcontrollerlist", new GetControllerListServiceRequestHandler());
            RequestHandlers.Add("commit", new CommitServiceRequestHandler());
        }
        
        public static void Start()
        {
            Current.InstanceStart();
        }
        
        protected virtual void InstanceStart()
        {
            Finsoft.Services.ApplicationServices.Initialize();
        }
        
        public static void Stop()
        {
            Current.InstanceStop();
        }
        
        protected virtual void InstanceStop()
        {
        }
        
        public static void SessionStart()
        {
            // The line below will prevent intermittent error “Session state has created a session id,
            // but cannot save it because the response was already flushed by the application.”
            var sessionId = HttpContext.Current.Session.SessionID;
            Current.UserSessionStart();
        }
        
        protected virtual void UserSessionStart()
        {
        }
        
        public static void SessionStop()
        {
            Current.UserSessionStop();
        }
        
        protected virtual void UserSessionStop()
        {
        }
        
        public static void Error()
        {
            var context = HttpContext.Current;
            if (context != null)
            	Current.HandleError(context, context.Server.GetLastError());
        }
        
        protected virtual void HandleError(HttpContext context, Exception er)
        {
        }
        
        public virtual object HandleException(JObject result, Exception ex)
        {
            while (ex.InnerException != null)
            	ex = ex.InnerException;
            var er = new ServiceRequestError();
            er.Message = ex.Message;
            er.ExceptionType = ex.GetType().ToString();
            var current = HttpContext.Current;
            if (current.Request.Url.Host.Equals("localhost") || !HttpContext.Current.IsCustomErrorEnabled)
            	er.StackTrace = ex.StackTrace;
            return er;
        }
        
        public virtual void RegisterContentServices(RouteCollection routes)
        {
            GenericRoute.Map(routes, new PlaceholderHandler(), "placeholder/{FileName}");
            routes.MapPageRoute("SiteContent", "{*url}", "~/Site.aspx");
            routes.MapPageRoute("DataControllerService", "{*url}", AquariumExtenderBase.DefaultServicePath);
        }
        
        public virtual void RegisterIgnoredRoutes(RouteCollection routes)
        {
            routes.Ignore("{handler}.ashx");
            routes.Ignore("favicon.ico");
            routes.Ignore("controlhost.aspx");
            routes.Ignore("charthost.aspx");
            routes.Ignore("{resource}.axd/{*pathInfo}");
            routes.Ignore("daf/{service}/{*methodName}");
            routes.Ignore("app_themes/{themeFolder}/{file}");
            routes.Ignore("{id}/arterySignalR/{*pathInfo}");
            if (!IsSiteContentEnabled)
            {
                routes.Ignore("images/{*pathInfo}");
                routes.Ignore("documents/{*pathInfo}");
                routes.Ignore("download/{*pathInfo}");
            }
            routes.Ignore("css/{*pathInfo}", new AddonRouteIgnoreConstraint());
            routes.Ignore("js/{*pathInfo}", new AddonRouteIgnoreConstraint());
            routes.Ignore("services/{*pathInfo}");
        }
        
        public static SortedDictionary<string, string> LoadContent()
        {
            var content = new SortedDictionary<string, string>();
            Create().LoadContent(HttpContext.Current.Request, HttpContext.Current.Response, content);
            string rawContent = null;
            if (content.TryGetValue("File", out rawContent))
            {
                // find the head
                var headMatch = Regex.Match(rawContent, "<head>([\\s\\S]+?)</head>");
                if (headMatch.Success)
                {
                    var head = headMatch.Groups[1].Value;
                    head = Regex.Replace(head, "\\s*<meta charset=\".+\"\\s*/?>\\s*", string.Empty);
                    content["Head"] = Regex.Replace(head, "\\s*<title>([\\S\\s]*?)</title>\\s*", string.Empty);
                    // find the title
                    var titleMatch = Regex.Match(head, "<title>(?\'Title\'[\\S\\s]+?)</title>");
                    if (titleMatch.Success)
                    {
                        var title = titleMatch.Groups["Title"].Value;
                        content["PageTitle"] = title;
                        content["PageTitleContent"] = title;
                    }
                    // find "about"
                    var aboutMatch = Regex.Match(head, "<meta\\s+name\\s*=\\s*\"description\"\\s+content\\s*=\\s*\"([\\s\\S]+?)\"\\s*/>");
                    if (aboutMatch.Success)
                    	content["About"] = HttpUtility.HtmlDecode(aboutMatch.Groups[1].Value);
                }
                // find the body
                var bodyMatch = Regex.Match(rawContent, "<body(?\'Attr\'[\\s\\S]*?)>(?\'Body\'[\\s\\S]+?)</body>");
                if (bodyMatch.Success)
                {
                    content["PageContent"] = EnrichData(bodyMatch.Groups["Body"].Value);
                    content["BodyAttributes"] = bodyMatch.Groups["Attr"].Value;
                }
                else
                	content["PageContent"] = EnrichData(rawContent);
            }
            return content;
        }
        
        public static string EnrichData(string body)
        {
            if (!(Regex.IsMatch(body, "<div[\\s\\S]+?(data-(app-role|controller|user-control|placeholder))\\s*=\"[\\s\\S]+?>")))
            	body = string.Format("<div data-app-role=\"page\" data-content-framework=\"bootstrap\">{0}</div>", body);
            return Regex.Replace(body, "(<script[^>]*(data-)?type=\"(\\$app\\.)?execute\"[^>]*>(?<Script>(.|\\n)*?)<\\/script>)" +
                    "", DoEnrichData);
        }
        
        private static string DoEnrichData(Match m)
        {
            try
            {
                var json = m.Groups["Script"].Value.Trim().Trim(')', '(', ';');
                var obj = JObject.Parse(json);
                var request = new PageRequest();
                request.Controller = ((string)(obj["controller"]));
                request.View = ((string)(obj["view"]));
                request.PageIndex = Convert.ToInt32(obj["pageIndex"]);
                request.PageSize = Convert.ToInt32(obj["pageSize"]);
                if (request.PageSize == 0)
                	request.PageSize = 100;
                request.SortExpression = ((string)(obj["sortExpression"]));
                var metadataFilter = ((JArray)(obj["metadataFilter"]));
                if (metadataFilter != null)
                	request.MetadataFilter = metadataFilter.ToObject<string[]>();
                else
                	request.MetadataFilter = new string[] {
                            "fields"};
                request.RequiresMetaData = true;
                var page = ControllerFactory.CreateDataController().GetPage(request.Controller, request.View, request);
                var output = ApplicationServices.CompressViewPageJsonOutput(JsonConvert.SerializeObject(page));
                var doFormat = obj["format"];
                if (doFormat == null)
                	doFormat = "true";
                var id = obj["id"];
                if (id == null)
                	id = request.Controller;
                return string.Format("<script>$app.data({{\"id\":\"{0}\",\"format\":{1},\"d\":{2}}});</script>", id, Convert.ToBoolean(doFormat).ToString().ToLower(), output);
            }
            catch (Exception ex)
            {
                return (("<div class=\"well text-danger\">" + ex.Message) 
                            + "</div>");
            }
        }
        
        public virtual string GetSiteContentControllerName()
        {
            return FrameworkSiteContentControllerName;
        }
        
        public virtual string GetSiteContentViewId()
        {
            return "editForm1";
        }
        
        public virtual string[] GetSiteContentEditors()
        {
            return new string[] {
                    "Administrators",
                    "Content Editors",
                    "Developers"};
        }
        
        public virtual string[] GetSiteContentDevelopers()
        {
            return new string[] {
                    "Administrators",
                    "Developers"};
        }
        
        public virtual string[] GetSuperUsers()
        {
            return new string[] {
                    "Administrators"};
        }
        
        public virtual void AfterAction(ActionArgs args, ActionResult result)
        {
        }
        
        public virtual void BeforeAction(ActionArgs args, ActionResult result)
        {
            if ((args.Controller == SiteContentControllerName) && !args.IgnoreBusinessRules)
            {
                var userIsDeveloper = IsDeveloper;
                if ((!IsContentEditor || !userIsDeveloper) || (args.Values == null))
                	throw new HttpException(403, "Forbidden");
                var id = args.SelectFieldValueObject(SiteContentFieldName(SiteContentFields.SiteContentId));
                var path = args.SelectFieldValueObject(SiteContentFieldName(SiteContentFields.Path));
                var fileName = args.SelectFieldValueObject(SiteContentFieldName(SiteContentFields.DataFileName));
                var text = args.SelectFieldValueObject(SiteContentFieldName(SiteContentFields.Text));
                // verify "Path" access
                if ((path == null) || (fileName == null))
                	throw new HttpException(403, "Forbidden");
                if (((path.Value != null) && path.Value.ToString().StartsWith("sys/", StringComparison.CurrentCultureIgnoreCase)) && !userIsDeveloper)
                	throw new HttpException(403, "Forbidden");
                if (((path.OldValue != null) && path.OldValue.ToString().StartsWith("sys/", StringComparison.CurrentCultureIgnoreCase)) && !userIsDeveloper)
                	throw new HttpException(403, "Forbidden");
                // convert and parse "Text" as needed
                if ((text != null) && args.CommandName != "Delete")
                {
                    var s = Convert.ToString(text.Value);
                    if (s == "$Text")
                    {
                        var fullPath = Convert.ToString(path.Value);
                        if (!(string.IsNullOrEmpty(fullPath)))
                        	fullPath = (fullPath + "/");
                        fullPath = (fullPath + Convert.ToString(fileName.Value));
                        if (!(fullPath.StartsWith("/")))
                        	fullPath = ("/" + fullPath);
                        if (!(fullPath.EndsWith(".html", StringComparison.CurrentCultureIgnoreCase)))
                        	fullPath = (fullPath + ".html");
                        var physicalPath = HttpContext.Current.Server.MapPath(("~" + fullPath));
                        if (!(File.Exists(physicalPath)))
                        {
                            physicalPath = HttpContext.Current.Server.MapPath(("~" + fullPath.Replace("-", string.Empty)));
                            if (!(File.Exists(physicalPath)))
                            	physicalPath = null;
                        }
                        if (!(string.IsNullOrEmpty(physicalPath)))
                        	text.NewValue = File.ReadAllText(physicalPath);
                    }
                }
            }
        }
        
        public virtual string SiteContentFieldName(SiteContentFields field)
        {
            if (field == SiteContentFields.SiteContentId)
            	return "SiteContentID";
            if (field == SiteContentFields.DataFileName)
            	return "FileName";
            if (field == SiteContentFields.DataContentType)
            	return "ContentType";
            return field.ToString();
        }
        
        public virtual string ReadSiteContentString(string relativePath)
        {
            var data = ReadSiteContentBytes(relativePath);
            if (data == null)
            	return null;
            return Encoding.UTF8.GetString(data);
        }
        
        public virtual byte[] ReadSiteContentBytes(string relativePath)
        {
            var f = ReadSiteContent(relativePath);
            if (f == null)
            	return null;
            return f.Data;
        }
        
        public virtual SiteContentFile ReadSiteContent(string relativePath)
        {
            var context = HttpContext.Current;
            var f = ((SiteContentFile)(context.Items[relativePath]));
            if (f == null)
            	f = ((SiteContentFile)(context.Cache[relativePath]));
            if (f == null)
            {
                var path = relativePath;
                var fileName = relativePath;
                var index = relativePath.LastIndexOf("/");
                if (index >= 0)
                {
                    fileName = path.Substring((index + 1));
                    path = relativePath.Substring(0, index);
                }
                else
                	path = null;
                var files = ReadSiteContent(path, fileName, 1);
                if (files.Count == 1)
                {
                    f = files[0];
                    context.Items[relativePath] = f;
                    if (f.CacheDuration > 0)
                    	context.Cache.Add(relativePath, f, null, DateTime.Now.AddSeconds(f.CacheDuration), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
            }
            return f;
        }
        
        public virtual SiteContentFileList ReadSiteContent(string relativePath, string fileName)
        {
            return ReadSiteContent(relativePath, fileName, Int32.MaxValue);
        }
        
        public virtual SiteContentFileList ReadSiteContent(string relativePath, string fileName, int maxCount)
        {
            var result = new SiteContentFileList();
            if (IsSafeMode)
            	return result;
            // prepare a filter
            var dataFileNameField = SiteContentFieldName(SiteContentFields.DataFileName);
            var pathField = SiteContentFieldName(SiteContentFields.Path);
            var filter = new List<string>();
            string pathFilter = null;
            if (!(string.IsNullOrEmpty(relativePath)))
            {
                pathFilter = "{0}:={1}";
                var firstWildcardIndex = relativePath.IndexOf("%");
                if (firstWildcardIndex >= 0)
                {
                    var lastWildcardIndex = relativePath.LastIndexOf("%");
                    pathFilter = "{0}:$contains${1}";
                    if (firstWildcardIndex == lastWildcardIndex)
                    	if (firstWildcardIndex == 0)
                        {
                            pathFilter = "{0}:$endswith${1}";
                            relativePath = relativePath.Substring(1);
                        }
                        else
                        	if (lastWildcardIndex == (relativePath.Length - 1))
                            {
                                pathFilter = "{0}:$beginswith${1}";
                                relativePath = relativePath.Substring(0, lastWildcardIndex);
                            }
                }
            }
            else
            	pathFilter = "{0}:=null";
            string fileNameFilter = null;
            if (!(string.IsNullOrEmpty(fileName)) && !((fileName == "%")))
            {
                fileNameFilter = "{0}:={1}";
                var firstWildcardIndex = fileName.IndexOf("%");
                if (firstWildcardIndex >= 0)
                {
                    var lastWildcardIndex = fileName.LastIndexOf("%");
                    fileNameFilter = "{0}:$contains${1}";
                    if (firstWildcardIndex == lastWildcardIndex)
                    	if (firstWildcardIndex == 0)
                        {
                            fileNameFilter = "{0}:$endswith${1}";
                            fileName = fileName.Substring(1);
                        }
                        else
                        	if (lastWildcardIndex == (fileName.Length - 1))
                            {
                                fileNameFilter = "{0}:$beginswith${1}";
                                fileName = fileName.Substring(0, lastWildcardIndex);
                            }
                }
            }
            if (!(string.IsNullOrEmpty(pathFilter)) || !(string.IsNullOrEmpty(fileNameFilter)))
            {
                filter.Add("_match_:$all$");
                if (!(string.IsNullOrEmpty(pathFilter)))
                	filter.Add(string.Format(pathFilter, pathField, DataControllerBase.ValueToString(relativePath)));
                if (fileName != null && !((fileName == "%")))
                {
                    filter.Add(string.Format(fileNameFilter, dataFileNameField, DataControllerBase.ValueToString(fileName)));
                    if (string.IsNullOrEmpty(Path.GetExtension(fileName)) && (string.IsNullOrEmpty(relativePath) || (!(relativePath.StartsWith("sys/", StringComparison.OrdinalIgnoreCase)) || relativePath.StartsWith("sys/controls", StringComparison.OrdinalIgnoreCase))))
                    {
                        filter.Add("_match_:$all$");
                        if (!(string.IsNullOrEmpty(pathFilter)))
                        	filter.Add(string.Format(pathFilter, pathField, DataControllerBase.ValueToString(relativePath)));
                        filter.Add(string.Format(fileNameFilter, dataFileNameField, DataControllerBase.ValueToString((Path.GetFileNameWithoutExtension(fileName).Replace("-", string.Empty) + ".html"))));
                    }
                }
            }
            //  determine user identity
            var context = HttpContext.Current;
            var userName = string.Empty;
            var isAuthenticated = false;
            var user = context.User;
            if (user != null)
            {
                userName = user.Identity.Name.ToLower();
                isAuthenticated = user.Identity.IsAuthenticated;
            }
            // enumerate site content files
            var r = new PageRequest();
            r.Controller = GetSiteContentControllerName();
            r.View = GetSiteContentViewId();
            r.RequiresSiteContentText = true;
            r.PageSize = Int32.MaxValue;
            r.Filter = filter.ToArray();
            var blobsToResolve = new SortedDictionary<string, SiteContentFile>();
            var access = Controller.GrantFullAccess("SiteContent");
            try
            {
                var engine = ControllerFactory.CreateDataEngine();
                var controller = ((DataControllerBase)(engine));
                var reader = engine.ExecuteReader(r);
                // verify optional SiteContent fields
                var fieldDictionary = new SortedDictionary<string, string>();
                for (var i = 0; (i < reader.FieldCount); i++)
                {
                    var fieldName = reader.GetName(i);
                    fieldDictionary[fieldName] = fieldName;
                }
                string rolesField = null;
                fieldDictionary.TryGetValue(SiteContentFieldName(SiteContentFields.Roles), out rolesField);
                string usersField = null;
                fieldDictionary.TryGetValue(SiteContentFieldName(SiteContentFields.Users), out usersField);
                string roleExceptionsField = null;
                fieldDictionary.TryGetValue(SiteContentFieldName(SiteContentFields.RoleExceptions), out roleExceptionsField);
                string userExceptionsField = null;
                fieldDictionary.TryGetValue(SiteContentFieldName(SiteContentFields.UserExceptions), out userExceptionsField);
                string cacheProfileField = null;
                fieldDictionary.TryGetValue(SiteContentFieldName(SiteContentFields.CacheProfile), out cacheProfileField);
                string scheduleField = null;
                fieldDictionary.TryGetValue(SiteContentFieldName(SiteContentFields.Schedule), out scheduleField);
                string scheduleExceptionsField = null;
                fieldDictionary.TryGetValue(SiteContentFieldName(SiteContentFields.ScheduleExceptions), out scheduleExceptionsField);
                var dataField = controller.CreateViewPage().FindField(SiteContentFieldName(SiteContentFields.Data));
                var blobHandler = dataField.OnDemandHandler;
                var wr = WorkflowRegister.GetCurrent(relativePath);
                // read SiteContent files
                while (reader.Read())
                {
                    // verify user access rights
                    var include = true;
                    if (!(string.IsNullOrEmpty(rolesField)))
                    {
                        var roles = Convert.ToString(reader[rolesField]);
                        if (!(string.IsNullOrEmpty(roles)) && !((roles == "?")))
                        	if ((roles == "*") && !isAuthenticated)
                            	include = false;
                            else
                            	if (!isAuthenticated || (!((roles == "*")) && !(DataControllerBase.UserIsInRole(roles))))
                                	include = false;
                    }
                    if (include && !(string.IsNullOrEmpty(usersField)))
                    {
                        var users = Convert.ToString(reader[usersField]);
                        if (!(string.IsNullOrEmpty(users)) && (Array.IndexOf(users.ToLower().Split(new char[] {
                                                    ','}, StringSplitOptions.RemoveEmptyEntries), userName) == -1))
                        	include = false;
                    }
                    if (include && !(string.IsNullOrEmpty(roleExceptionsField)))
                    {
                        var roleExceptions = Convert.ToString(reader[roleExceptionsField]);
                        if (!(string.IsNullOrEmpty(roleExceptions)) && (isAuthenticated && ((roleExceptions == "*") || DataControllerBase.UserIsInRole(roleExceptions))))
                        	include = false;
                    }
                    if (include && !(string.IsNullOrEmpty(userExceptionsField)))
                    {
                        var userExceptions = Convert.ToString(reader[userExceptionsField]);
                        if (!(string.IsNullOrEmpty(userExceptions)) && !((Array.IndexOf(userExceptions.ToLower().Split(new char[] {
                                                    ','}, StringSplitOptions.RemoveEmptyEntries), userName) == -1)))
                        	include = false;
                    }
                    var physicalName = Convert.ToString(reader[dataFileNameField]);
                    var physicalPath = Convert.ToString(reader[SiteContentFieldName(SiteContentFields.Path)]);
                    // check if the content object is a part of a workflow
                    if (((wr != null) && wr.Enabled) && !(wr.IsMatch(physicalPath, physicalName)))
                    	include = false;
                    string schedule = null;
                    string scheduleExceptions = null;
                    // check if the content object is on schedule
                    if (include && (string.IsNullOrEmpty(physicalPath) || !(physicalPath.StartsWith("sys/schedules/"))))
                    {
                        if (!(string.IsNullOrEmpty(scheduleField)))
                        	schedule = Convert.ToString(reader[scheduleField]);
                        if (!(string.IsNullOrEmpty(scheduleExceptionsField)))
                        	scheduleExceptions = Convert.ToString(reader[scheduleExceptionsField]);
                        if (!(string.IsNullOrEmpty(schedule)) || !(string.IsNullOrEmpty(scheduleExceptions)))
                        {
                            var scheduleStatusKey = string.Format("ScheduleStatus|{0}|{1}", schedule, scheduleExceptions);
                            var status = ((ScheduleStatus)(context.Items[scheduleStatusKey]));
                            if (status == null)
                            	status = ((ScheduleStatus)(context.Cache[scheduleStatusKey]));
                            var scheduleStatusChanged = false;
                            if (status == null)
                            {
                                if (!(string.IsNullOrEmpty(schedule)) && !(schedule.Contains("+")))
                                	schedule = ReadSiteContentString(("sys/schedules%/" + schedule));
                                if (!(string.IsNullOrEmpty(scheduleExceptions)) && !(scheduleExceptions.Contains("+")))
                                	scheduleExceptions = ReadSiteContentString(("sys/schedules%/" + scheduleExceptions));
                                if (!(string.IsNullOrEmpty(schedule)) || !(string.IsNullOrEmpty(scheduleExceptions)))
                                	status = Scheduler.Test(schedule, scheduleExceptions);
                                else
                                {
                                    status = new ScheduleStatus();
                                    status.Success = true;
                                    status.NextTestDate = DateTime.MaxValue;
                                }
                                context.Items[scheduleStatusKey] = status;
                                scheduleStatusChanged = true;
                            }
                            else
                            	if (DateTime.Now > status.NextTestDate)
                                {
                                    status = Scheduler.Test(status.Schedule, status.Exceptions);
                                    context.Items[scheduleStatusKey] = status;
                                    scheduleStatusChanged = true;
                                }
                            if (scheduleStatusChanged)
                            	context.Cache.Add(scheduleStatusKey, status, null, DateTime.Now.AddSeconds(ScheduleCacheDuration), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                            if (!status.Success)
                            	include = false;
                        }
                    }
                    // create a file instance
                    if (include)
                    {
                        var siteContentIdField = SiteContentFieldName(SiteContentFields.SiteContentId);
                        var f = new SiteContentFile();
                        f.Id = reader[siteContentIdField];
                        f.Name = fileName;
                        f.PhysicalName = physicalName;
                        if (string.IsNullOrEmpty(f.Name) || f.Name.Contains("%"))
                        	f.Name = f.PhysicalName;
                        f.Path = physicalPath;
                        f.ContentType = Convert.ToString(reader[SiteContentFieldName(SiteContentFields.DataContentType)]);
                        f.Schedule = schedule;
                        f.ScheduleExceptions = scheduleExceptions;
                        if (!(string.IsNullOrEmpty(cacheProfileField)))
                        {
                            var cacheProfile = Convert.ToString(reader[cacheProfileField]);
                            if (!(string.IsNullOrEmpty(cacheProfile)))
                            {
                                f.CacheProfile = cacheProfile;
                                cacheProfile = ReadSiteContentString(("sys/cache-profiles/" + cacheProfile));
                                if (!(string.IsNullOrEmpty(cacheProfile)))
                                {
                                    var m = NameValueListRegex.Match(cacheProfile);
                                    while (m.Success)
                                    {
                                        var n = m.Groups["Name"].Value.ToLower();
                                        var v = m.Groups["Value"].Value;
                                        if (n == "duration")
                                        {
                                            var duration = 0;
                                            if (Int32.TryParse(v, out duration))
                                            {
                                                f.CacheDuration = duration;
                                                f.CacheLocation = HttpCacheability.ServerAndPrivate;
                                            }
                                        }
                                        else
                                        	if (n == "location")
                                            	try
                                                {
                                                    f.CacheLocation = ((HttpCacheability)(TypeDescriptor.GetConverter(typeof(HttpCacheability)).ConvertFromString(v)));
                                                }
                                                catch (Exception)
                                                {
                                                }
                                            else
                                            	if (n == "varybyheaders")
                                                	f.CacheVaryByHeaders = v.Split(new char[] {
                                                                ',',
                                                                ';'}, StringSplitOptions.RemoveEmptyEntries);
                                                else
                                                	if (n == "varybyparams")
                                                    	f.CacheVaryByParams = v.Split(new char[] {
                                                                    ',',
                                                                    ';'}, StringSplitOptions.RemoveEmptyEntries);
                                                    else
                                                    	if (n == "nostore")
                                                        	f.CacheNoStore = (v.ToLower() == "true");
                                        m = m.NextMatch();
                                    }
                                }
                            }
                        }
                        var textString = reader[SiteContentFieldName(SiteContentFields.Text)];
                        if (DBNull.Value.Equals(textString) || !f.IsText)
                        {
                            var blobKey = string.Format("{0}=o|{1}", blobHandler, f.Id);
                            if (f.CacheDuration > 0)
                            	f.Data = ((byte[])(HttpContext.Current.Cache[blobKey]));
                            if (f.Data == null)
                            	blobsToResolve[blobKey] = f;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(f.ContentType))
                            	if (Regex.IsMatch(((string)(textString)), "</\\w+\\s*>"))
                                	f.ContentType = "text/xml";
                                else
                                	f.ContentType = "text/plain";
                            f.Data = Encoding.UTF8.GetBytes(((string)(textString)));
                        }
                        result.Add(f);
                        if (result.Count == maxCount)
                        	break;
                    }
                }
                reader.Close();
            }
            finally
            {
                Controller.RevokeFullAccess(access);
            }
            foreach (var blobKey in blobsToResolve.Keys)
            {
                var f = blobsToResolve[blobKey];
                // download blob content
                try
                {
                    access = Controller.GrantFullAccess("SiteContent");
                    try
                    {
                        f.Data = Blob.Read(blobKey);
                    }
                    finally
                    {
                        Controller.RevokeFullAccess(access);
                    }
                    if (f.CacheDuration > 0)
                    	HttpContext.Current.Cache.Add(blobKey, f.Data, null, DateTime.Now.AddSeconds(f.CacheDuration), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    f.Error = ex.Message;
                }
            }
            return result;
        }
        
        public virtual bool IsSystemResource(HttpRequest request)
        {
            return SystemResourceRegex.IsMatch(request.AppRelativeCurrentExecutionFilePath);
        }
        
        public virtual string AddScripts()
        {
            if (Addons.Count == 0)
            	return string.Empty;
            var sb = new StringBuilder();
            foreach (var addon in Addons)
            	sb.Append(((string)(addon.GetType().GetMethod("Script").Invoke(addon, null))));
            return sb.ToString();
        }
        
        public virtual string AddStyleSheets()
        {
            if (Addons.Count == 0)
            	return string.Empty;
            var sb = new StringBuilder();
            foreach (var addon in Addons)
            	sb.Append(((string)(addon.GetType().GetMethod("StyleSheet").Invoke(addon, null))));
            return sb.ToString();
        }
        
        public virtual void LoadContent(HttpRequest request, HttpResponse response, SortedDictionary<string, string> content)
        {
            if (IsSystemResource(request))
            	return;
            if (request.Url.LocalPath == "/js/daf/add.min.js")
            {
                response.Cache.SetExpires(DateTime.Now.AddMonths(1));
                response.Cache.SetCacheability(HttpCacheability.Public);
                response.ContentType = "text/javascript";
                response.Write(AddScripts());
                try
                {
                    response.Flush();
                }
                catch (Exception)
                {
                }
                response.End();
            }
            if (request.Url.LocalPath == "/css/daf/add.min.css")
            {
                response.Cache.SetExpires(DateTime.Now.AddMonths(1));
                response.Cache.SetCacheability(HttpCacheability.Public);
                response.ContentType = "text/css";
                response.Write(AddStyleSheets());
                try
                {
                    response.Flush();
                }
                catch (Exception)
                {
                }
                response.End();
            }
            string text = null;
            var tryFileSystem = true;
            var addonPage = Regex.Match(request.Url.LocalPath, "^\\/_(\\w+)");
            if (addonPage.Success)
            	foreach (var addon in Addons)
                	if (addon.GetType().Name.Equals(addonPage.Groups[1].Value, StringComparison.OrdinalIgnoreCase))
                    {
                        text = ((string)(addon.GetType().GetMethod("Page").Invoke(addon, null)));
                        tryFileSystem = false;
                        break;
                    }
            if (IsSiteContentEnabled && tryFileSystem)
            {
                var fileName = HttpUtility.UrlDecode(request.Url.Segments[(request.Url.Segments.Length - 1)]);
                var path = request.CurrentExecutionFilePath.Substring(request.ApplicationPath.Length);
                if ((fileName == "/") && string.IsNullOrEmpty(path))
                	fileName = "index";
                else
                	if (!(string.IsNullOrEmpty(path)))
                    {
                        path = path.Substring(0, (path.Length - fileName.Length));
                        if (path.EndsWith("/"))
                        	path = path.Substring(0, (path.Length - 1));
                    }
                if (string.IsNullOrEmpty(path))
                	path = null;
                var files = ReadSiteContent(path, fileName, 1);
                if (files.Count > 0)
                {
                    var f = files[0];
                    if (f.ContentType == "text/html")
                    {
                        text = f.Text;
                        tryFileSystem = false;
                    }
                    else
                    {
                        if (f.CacheDuration > 0)
                        {
                            var expires = DateTime.Now.AddSeconds(f.CacheDuration);
                            response.Cache.SetExpires(expires);
                            response.Cache.SetCacheability(f.CacheLocation);
                            if (f.CacheVaryByParams != null)
                            	foreach (var header in f.CacheVaryByParams)
                                	response.Cache.VaryByParams[header] = true;
                            if (f.CacheVaryByHeaders != null)
                            	foreach (var header in f.CacheVaryByHeaders)
                                	response.Cache.VaryByHeaders[header] = true;
                            if (f.CacheNoStore)
                            	response.Cache.SetNoStore();
                        }
                        response.ContentType = f.ContentType;
                        response.AddHeader("Content-Disposition", ("filename=" + HttpUtility.UrlEncode(f.PhysicalName)));
                        response.OutputStream.Write(f.Data, 0, f.Data.Length);
                        try
                        {
                            response.Flush();
                        }
                        catch (Exception)
                        {
                        }
                        response.End();
                    }
                }
            }
            if (tryFileSystem)
            {
                var filePath = request.PhysicalPath;
                var fileExtension = Path.GetExtension(filePath);
                if (!((fileExtension.ToLower() == ".html")) && File.Exists(filePath))
                {
                    var fileName = Path.GetFileName(filePath);
                    response.AddHeader("Content-Disposition", ("filename=" + HttpUtility.UrlEncode(fileName)));
                    response.ContentType = MimeMapping.GetMimeMapping(fileName);
                    var expires = DateTime.Now.AddSeconds(((60 * 60) 
                                    * 24));
                    response.Cache.SetExpires(expires);
                    response.Cache.SetCacheability(HttpCacheability.Public);
                    var data = File.ReadAllBytes(filePath);
                    response.OutputStream.Write(data, 0, data.Length);
                    try
                    {
                        response.Flush();
                    }
                    catch (Exception)
                    {
                    }
                    response.End();
                }
                if (!(string.IsNullOrEmpty(fileExtension)))
                	filePath = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath));
                filePath = (filePath + ".html");
                if (File.Exists(filePath))
                	text = File.ReadAllText(filePath);
                else
                	if (Path.GetFileNameWithoutExtension(filePath).Contains("-"))
                    {
                        filePath = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileName(filePath).Replace("-", string.Empty));
                        if (File.Exists(filePath))
                        	text = File.ReadAllText(filePath);
                    }
                if (text != null)
                	text = Localizer.Replace("Pages", filePath, text);
            }
            if (text != null)
            {
                text = Regex.Replace(text, "<!--[\\s\\S]+?-->\\s*", string.Empty);
                content["File"] = text;
            }
        }
        
        public virtual void CreateStandardMembershipAccounts()
        {
            // Create a separate code file with a definition of the partial class ApplicationServices overriding
            // this method to prevent automatic registration of 'admin' and 'user'. Do not change this file directly.
            RegisterStandardMembershipAccounts();
        }
        
        public virtual bool RequiresAuthentication(HttpRequest request)
        {
            if (request.Path.EndsWith("Export.ashx", StringComparison.CurrentCultureIgnoreCase))
            {
                var formToken = HttpContext.Current.Request.Params["t"];
                if (string.IsNullOrEmpty(formToken) || !(ValidateToken(formToken)))
                	return true;
            }
            return false;
        }
        
        public virtual bool AuthenticateRequest(HttpContext context)
        {
            return false;
        }
        
        public virtual void RedirectToLoginPage()
        {
            var handler = OAuthHandlerFactory.GetActiveHandler();
            if (handler != null)
            {
                handler.StartPage = HttpContext.Current.Request.Url.AbsolutePath;
                handler.RedirectToLoginPage();
                return;
            }
            FormsAuthentication.RedirectToLoginPage();
        }
        
        public virtual object AuthenticateUser(string username, string password, bool createPersistentCookie)
        {
            var response = HttpContext.Current.Response;
            if (password.StartsWith("token:"))
            {
                // validate token login
                try
                {
                    var key = password.Substring(6);
                    var ticket = FormsAuthentication.Decrypt(key);
                    if (ValidateTicket(ticket) && (!(string.IsNullOrEmpty(ticket.UserData)) && Regex.IsMatch(ticket.UserData, "^(REFRESHONLY$|OAUTH:)")))
                    {
                        var user = Membership.GetUser(ticket.Name);
                        if ((user != null) && (user.IsApproved && !user.IsLockedOut))
                        {
                            InvalidateTicket(ticket);
                            var cookie = new HttpCookie(".PROVIDER", string.Empty);
                            if (!(string.IsNullOrEmpty(ticket.UserData)) && ticket.UserData.StartsWith("OAUTH:"))
                            {
                                var handler = OAuthHandlerFactory.Create(ticket.UserData.Substring(6));
                                if (handler != null)
                                {
                                    cookie.Value = handler.GetHandlerName();
                                    if (!(handler.ValidateRefreshToken(user, key)))
                                    	return false;
                                }
                            }
                            HttpContext.Current.Response.SetCookie(cookie);
                            FormsAuthentication.SetAuthCookie(user.UserName, createPersistentCookie);
                            return CreateTicket(user, key);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                // login user
                if (UserLogin(username, password, createPersistentCookie))
                {
                    FormsAuthentication.SetAuthCookie(username, createPersistentCookie);
                    var user = Membership.GetUser(username);
                    if (user != null)
                    	return CreateTicket(user, null);
                }
            }
            return false;
        }
        
        public virtual UserTicket CreateTicket(MembershipUser user, string refreshToken)
        {
            var accessTimeout = 15;
            var refreshTimeout = (60 
                        * (24 * 7));
            var jTimeout = TryGetJsonProperty(DefaultSettings, "membership.accountManager.accessTokenDuration");
            if (jTimeout != null)
            	accessTimeout = ((int)(jTimeout));
            jTimeout = TryGetJsonProperty(DefaultSettings, "membership.accountManager.refreshTokenDuration");
            if (jTimeout != null)
            	refreshTimeout = ((int)(jTimeout));
            var userData = string.Empty;
            var handler = OAuthHandlerFactory.GetActiveHandler();
            if (handler != null)
            	userData = ("OAUTH:" + handler.GetHandlerName());
            var accessTicket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(accessTimeout), false, userData);
            if (string.IsNullOrEmpty(refreshToken))
            {
                var refreshTicket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(refreshTimeout), false, "REFRESHONLY");
                refreshToken = FormsAuthentication.Encrypt(refreshTicket);
            }
            return new UserTicket(user, FormsAuthentication.Encrypt(accessTicket), refreshToken);
        }
        
        public virtual bool ValidateTicket(FormsAuthenticationTicket ticket)
        {
            return !(((ticket == null) || (ticket.Expired || string.IsNullOrEmpty(ticket.Name))));
        }
        
        public virtual void InvalidateTicket(FormsAuthenticationTicket ticket)
        {
        }
        
        public virtual bool ValidateToken(string accessToken)
        {
            try
            {
                var ticket = FormsAuthentication.Decrypt(accessToken);
                if (ValidateTicket(ticket))
                {
                    HttpContext.Current.User = new RolePrincipal(new FormsIdentity(new FormsAuthenticationTicket(ticket.Name, false, 10)));
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        
        public virtual bool UserLogin(string username, string password, bool createPersistentCookie)
        {
            if (Membership.ValidateUser(username, password))
            	return true;
            else
            	return false;
        }
        
        public virtual void UserLogout()
        {
            FormsAuthentication.SignOut();
            if (ApplicationServices.IsSiteContentEnabled)
            {
                var handler = OAuthHandlerFactory.GetActiveHandler();
                if (handler != null)
                	handler.SignOut();
            }
        }
        
        public virtual string[] UserRoles()
        {
            return Roles.GetRolesForUser();
        }
        
        public virtual JObject UserThemes()
        {
            var lists = new JObject();
            var themes = new JArray();
            var accents = new JArray();
            lists["themes"] = themes;
            lists["accents"] = accents;
            var themesPath = HttpContext.Current.Server.MapPath("~/css/themes");
            foreach (var f in Directory.GetFiles(themesPath, "touch-theme.*.json"))
            {
                var theme = JObject.Parse(File.ReadAllText(f));
                var t = new JObject();
                t["name"] = theme["name"];
                t["color"] = theme["color"];
                themes.Add(t);
            }
            foreach (var f in Directory.GetFiles(themesPath, "touch-accent.*.json"))
            {
                var accent = JObject.Parse(File.ReadAllText(f));
                var a = new JObject();
                a["name"] = accent["name"];
                a["color"] = accent["color"];
                accents.Add(a);
            }
            return lists;
        }
        
        public virtual JObject UserSettings(Page p)
        {
            var settings = new JObject(DefaultSettings);
            if (settings["membership"] == null)
            	settings["membership"] = new JObject();
            var userKey = string.Empty;
            MembershipUser user = null;
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            	user = Membership.GetUser();
            if (user != null)
            {
                userKey = Convert.ToString(user.ProviderUserKey);
                if (!(string.IsNullOrEmpty(user.Comment)))
                {
                    var m = Regex.Match(user.Comment, "\\bSource:\\s*\\b(?\'Value\'\\w+)\\b", RegexOptions.IgnoreCase);
                    if (m.Success)
                    {
                        var handler = OAuthHandlerFactory.Create(m.Groups["Value"].Value);
                        if (handler != null)
                        	settings["membership"]["profile"] = handler.GetUserProfile();
                    }
                }
            }
            settings["appInfo"] = string.Join("|", new string[] {
                        Name,
                        HttpContext.Current.User.Identity.Name,
                        userKey});
            if (IsContentEditor)
            {
                settings["siteContent"] = GetSiteContentControllerName();
                settings["siteContentPK"] = SiteContentFieldName(SiteContentFields.SiteContentId);
            }
            settings["rootUrl"] = p.ResolveUrl("~");
            settings["ui"]["theme"]["name"] = UserTheme;
            settings["ui"]["theme"]["accent"] = UserAccent;
            if (settings.ContainsKey("server"))
            	settings.Remove("server");
            return settings;
        }
        
        public virtual string UserHomePageUrl()
        {
            if (IsSiteContentEnabled)
            {
                var index = ReadSiteContent("index");
                if (index != null)
                	return HttpContext.Current.Request.ApplicationPath;
            }
            return "~/pages/home";
        }
        
        public virtual string UserPictureString(MembershipUser user)
        {
            try
            {
                var img = UserPictureImage(user);
                if (img == null)
                	img = UserPictureFromCMS(user);
                if (img != null)
                {
                    if ((img.Width > 80) || (img.Height > 80))
                    {
                        var scale = (((float)(img.Width)) / 80);
                        var height = ((int)((img.Height / scale)));
                        var width = 80;
                        if (img.Height < img.Width)
                        {
                            scale = (((float)(img.Height)) / 80);
                            height = 80;
                            width = ((int)((img.Width / scale)));
                        }
                        img = Blob.ResizeImage(img, width, height);
                    }
                    using (var stream = new MemoryStream())
                    {
                        img.Save(stream, ImageFormat.Bmp);
                        var bytes = stream.ToArray();
                        img.Dispose();
                        return ("data:image/raw;base64," + Convert.ToBase64String(bytes));
                    }
                }
            }
            catch (Exception)
            {
            }
            return string.Empty;
        }
        
        public virtual Image UserPictureImage(MembershipUser user)
        {
            var url = UserPictureUrl(user);
            if (!(string.IsNullOrEmpty(url)))
            {
                var request = WebRequest.Create(url);
                using (var stream = request.GetResponse().GetResponseStream())
                	using (var ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        return ((Image)(new ImageConverter().ConvertFrom(ms.ToArray())));
                    }
            }
            else
            {
                url = UserPictureFilePath(user);
                if (!(string.IsNullOrEmpty(url)))
                	return Image.FromFile(url);
            }
            return null;
        }
        
        public virtual Image UserPictureFromCMS(MembershipUser user)
        {
            if (IsSiteContentEnabled)
            {
                var list = ReadSiteContent("sys/users", (user.UserName + ".%"));
                foreach (var file in list)
                	if (file.ContentType.StartsWith("image/") && (file.Data != null))
                    	return ((Image)(new ImageConverter().ConvertFrom(file.Data)));
            }
            return null;
        }
        
        public virtual string UserPictureFilePath(MembershipUser user)
        {
            return null;
        }
        
        public virtual string UserPictureUrl(MembershipUser user)
        {
            return null;
        }
        
        public static ApplicationServices Create()
        {
            return new ApplicationServices();
        }
        
        public static bool UserIsAuthorizedToAccessResource(string path, string roles)
        {
            return !(Create().ResourceAuthorizationIsRequired(path, roles));
        }
        
        public virtual bool ResourceAuthorizationIsRequired(string path, string roles)
        {
            if (!AuthorizationIsSupported)
            	return false;
            if (roles == null)
            	roles = string.Empty;
            else
            	roles = roles.Trim();
            var acl = AccessControlList.Current;
            var appPage = Regex.Match(path, "pages/(.+?)(\\?|$)", RegexOptions.IgnoreCase);
            if ((appPage.Success && !(acl.PermissionGranted(PermissionKind.Page, appPage.Groups[1].Value))) && !(path.Equals(FormsAuthentication.LoginUrl)))
            {
                if (!(string.IsNullOrEmpty(roles)))
                {
                    var roleList = Regex.Split(roles, "\\s+|\\s*,\\s*");
                    var pageSupers = roleList.Intersect(SuperUsers).ToArray();
                    if ((pageSupers.Length > 0) && DataControllerBase.UserIsInRole(pageSupers))
                    	return false;
                    return true;
                }
                return true;
            }
            var requiresAuthorization = false;
            var isAuthenticated = HttpContext.Current.User.Identity.IsAuthenticated;
            if (!acl.Enabled)
            {
                if (string.IsNullOrEmpty(roles) && !isAuthenticated)
                	requiresAuthorization = true;
                if (!(string.IsNullOrEmpty(roles)) && !((roles == "?")))
                	if (roles == "*")
                    {
                        if (!isAuthenticated)
                        	requiresAuthorization = true;
                    }
                    else
                    	if (!isAuthenticated || !(DataControllerBase.UserIsInRole(roles)))
                        	requiresAuthorization = true;
            }
            if (path == FormsAuthentication.LoginUrl)
            {
                requiresAuthorization = false;
                if (!isAuthenticated && (!((HttpContext.Current.Request.QueryString["_autoLogin"] == "false")) && (HttpContext.Current.Request.Cookies[".TOKEN"] == null)))
                {
                    var handler = OAuthHandlerFactory.CreateAutoLogin();
                    if (handler != null)
                    {
                        HttpContext.Current.Response.Cookies.Set(new HttpCookie(".PROVIDER", handler.GetHandlerName()));
                        requiresAuthorization = true;
                    }
                }
            }
            return requiresAuthorization;
        }
        
        public static void RegisterStandardMembershipAccounts()
        {
            if (AuthorizationIsSupported)
            {
                var admin = Membership.GetUser("admin");
                if ((admin != null) && admin.IsLockedOut)
                	admin.UnlockUser();
                var user = Membership.GetUser("user");
                if ((user != null) && user.IsLockedOut)
                	user.UnlockUser();
                if (Membership.GetUser("admin") == null)
                {
                    MembershipCreateStatus status;
                    admin = Membership.CreateUser("admin", "admin123%", "admin@Finsoft.com", "ASP.NET", "Code OnTime", true, out status);
                    user = Membership.CreateUser("user", "user123%", "user@Finsoft.com", "ASP.NET", "Code OnTime", true, out status);
                    Roles.CreateRole("Administrators");
                    Roles.CreateRole("Users");
                    Roles.AddUserToRole(admin.UserName, "Users");
                    Roles.AddUserToRole(user.UserName, "Users");
                    Roles.AddUserToRole(admin.UserName, "Administrators");
                }
            }
        }
        
        public static void RegisterCssLinks(Page p)
        {
            var l = new HtmlLink();
            l.ID = "FinsoftTheme";
            l.Attributes.Add("type", "text/css");
            l.Attributes.Add("rel", "stylesheet");
            p.Header.Controls.Add(((Control)(l)));
            var services = ApplicationServices.Current;
            var jqmCss = string.Format("jquery.mobile-{0}.min.css", ApplicationServices.JqmVersion);
            l.Href = ("~/css/sys/" + jqmCss);
            var meta = new HtmlMeta();
            meta.Attributes["name"] = "viewport";
            meta.Attributes["content"] = "width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no";
            p.Header.Controls.AddAt(0, meta);
            if (ApplicationServices.EnableCombinedCss)
            {
                l.Href = p.ResolveUrl(string.Format("~/appservices/stylesheet-{0}.min.css?_t={1}.{2}&_r={3}&_cf=", ApplicationServices.Version, services.UserTheme, services.UserAccent, ApplicationServices.CombinedResourceType));
                l.Attributes["class"] = "app-theme";
            }
            else
            	foreach (var stylesheet in services.EnumerateTouchUIStylesheets())
                {
                    var cssName = Path.GetFileName(stylesheet);
                    if (!(cssName.StartsWith("jquery.mobile")) && !(cssName.StartsWith("bootstrap")))
                    {
                        var cssLink = new HtmlLink();
                        cssLink.Href = string.Format("{0}?{1}", stylesheet.Replace('\\', '/'), ApplicationServices.Version);
                        if (cssName.StartsWith("touch-theme."))
                        	cssLink.Attributes["class"] = "app-theme";
                        cssLink.Attributes["type"] = "text/css";
                        cssLink.Attributes["rel"] = "stylesheet";
                        p.Header.Controls.Add(cssLink);
                    }
                }
            var removeList = new List<Control>();
            foreach (var c2 in p.Header.Controls)
            	if (c2 is HtmlLink)
                {
                    l = ((HtmlLink)(c2));
                    if (l.Href.Contains("App_Themes/"))
                    	removeList.Add(l);
                }
            foreach (var c2 in removeList)
            	p.Header.Controls.Remove(c2);
        }
        
        private void LoadTheme()
        {
            var theme = string.Empty;
            if (HttpContext.Current != null)
            {
                var themeCookie = HttpContext.Current.Request.Cookies[(".COTTHEME" + BusinessRules.UserName)];
                if (themeCookie != null)
                	theme = themeCookie.Value;
            }
            if (!(string.IsNullOrEmpty(theme)) && theme.Contains('.'))
            {
                theme = theme.Replace(" ", string.Empty);
                var parts = theme.Split('.');
                _userTheme = parts[0];
                _userAccent = parts[1];
            }
            else
            {
                _userTheme = ((string)(DefaultSettings["ui"]["theme"]["name"]));
                _userAccent = ((string)(DefaultSettings["ui"]["theme"]["accent"]));
            }
        }
        
        protected virtual bool AllowTouchUIStylesheet(string name)
        {
            return !(Regex.IsMatch(name, "^(touch|bootstrap|jquery\\.mobile)"));
        }
        
        public virtual List<string> EnumerateTouchUIStylesheets()
        {
            var stylesheets = new List<string>();
            var ext = ".min.css";
            if (!EnableMinifiedCss)
            	ext = ".css";
            stylesheets.Add(string.Format("~\\css\\sys\\jquery.mobile-{0}{1}", ApplicationServices.JqmVersion, ext));
            stylesheets.Add(("~\\css\\daf\\touch" + ext));
            stylesheets.Add(("~\\css\\daf\\touch-charts" + ext));
            stylesheets.Add(("~\\css\\sys\\bootstrap" + ext));
            stylesheets.Add(string.Format("~\\appservices\\touch-theme.{0}.{1}.css", UserTheme, UserAccent));
            if (!(string.IsNullOrEmpty(AddStyleSheets())))
            	stylesheets.Add("~\\css\\daf\\add.min.css");
            // enumerate custom css files
            var customCss = ((List<string>)(HttpRuntime.Cache["IncludedCss"]));
            if (customCss == null)
            {
                customCss = new List<string>();
                var cssPath = Path.Combine(HttpRuntime.AppDomainAppPath, "css");
                CacheDependency dep = null;
                if (Directory.Exists(cssPath))
                {
                    dep = new FolderCacheDependency(cssPath, "*.css");
                    var ignorePath = Path.Combine(cssPath, "_ignore.txt");
                    Regex ignoreRegex = null;
                    if (File.Exists(ignorePath))
                    	ignoreRegex = BuildSearchPathRegex(File.ReadAllLines(ignorePath));
                    foreach (var filePath in Directory.EnumerateFiles(cssPath, "*.css", SearchOption.AllDirectories))
                    {
                        var css = Path.GetFileName(filePath);
                        var relativePath = ("~\\" + filePath.Substring(HttpRuntime.AppDomainAppPath.Length));
                        if (AllowTouchUIStylesheet(css) && ((ignoreRegex == null) || !(ignoreRegex.IsMatch(relativePath.Substring(2)))))
                        	if (!(css.EndsWith(".min.css")))
                            	customCss.Add(relativePath);
                            else
                            {
                                var index = customCss.IndexOf((css.Substring(0, (css.Length - 7)) + "css"));
                                if (index > -1)
                                	customCss[index] = relativePath;
                                else
                                	customCss.Add(relativePath);
                            }
                    }
                }
                HttpRuntime.Cache.Add("IncludedCss", customCss, dep, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            }
            stylesheets.AddRange(customCss);
            return stylesheets;
        }
        
        private static string DoReplaceCssUrl(Match m)
        {
            var header = m.Groups["Header"].Value;
            var name = m.Groups["Name"].Value;
            var symbol = m.Groups["Symbol"].Value;
            if (((name == "data") || name.StartsWith("http")) && (symbol == ":"))
            	return m.Value;
            var appPath = HttpContext.Current.Request.ApplicationPath;
            if (!(appPath.EndsWith("/")))
            	appPath = (appPath + "/");
            name = Regex.Replace(name, "^(\\.\\.\\/)+", appPath);
            return (header 
                        + (name + symbol));
        }
        
        public static string CombineTouchUIStylesheets(HttpContext context)
        {
            var response = context.Response;
            var cache = response.Cache;
            cache.SetCacheability(HttpCacheability.Public);
            cache.VaryByHeaders["User-Agent"] = true;
            cache.SetOmitVaryStar(true);
            cache.SetExpires(DateTime.Now.AddDays(365));
            cache.SetValidUntilExpires(true);
            cache.SetLastModifiedFromFileDependencies();
            // combine scripts
            var contentFramework = context.Request.QueryString["_cf"];
            var includeBootstrap = (contentFramework == "bootstrap");
            var sb = new StringBuilder();
            var services = Create();
            foreach (var stylesheet in services.EnumerateTouchUIStylesheets())
            {
                var cssName = Path.GetFileName(stylesheet);
                if (includeBootstrap || !(cssName.StartsWith("bootstrap")))
                	if (cssName.StartsWith("touch-theme."))
                    	sb.AppendLine(StylesheetGenerator.Compile(cssName));
                    else
                    {
                        string data = null;
                        if (stylesheet == "~\\css\\daf\\add.min.css")
                        	data = ApplicationServices.Current.AddStyleSheets();
                        else
                        	data = File.ReadAllText(HttpContext.Current.Server.MapPath(stylesheet));
                        data = CssUrlRegex.Replace(data, DoReplaceCssUrl);
                        if (!(data.Contains("@import url")))
                        	sb.AppendLine(data);
                        else
                        	sb.Insert(0, data);
                    }
            }
            return sb.ToString();
        }
        
        public virtual void ConfigureScripts(List<ScriptReference> scripts)
        {
            var jsPath = Path.Combine(HttpRuntime.AppDomainAppPath, "js");
            var includedScripts = ((List<string>)(HttpRuntime.Cache["IncludedScripts"]));
            if (includedScripts == null)
            {
                includedScripts = new List<string>();
                CacheDependency dep = null;
                if (Directory.Exists(jsPath))
                {
                    dep = new FolderCacheDependency(jsPath, "*.js");
                    var ignorePath = Path.Combine(jsPath, "_ignore.txt");
                    Regex ignoreRegex = null;
                    if (File.Exists(ignorePath))
                    	ignoreRegex = BuildSearchPathRegex(File.ReadAllLines(ignorePath));
                    foreach (var file in Directory.EnumerateFiles(jsPath, "*.js", SearchOption.AllDirectories))
                    {
                        var relativeFile = file.Substring((jsPath.Length + 1));
                        if (((ignoreRegex == null) || !(ignoreRegex.IsMatch(relativeFile))) && !(DefaultExcludeScriptRegex.IsMatch(relativeFile)))
                        	includedScripts.Add(("~/" + file.Substring(HttpRuntime.AppDomainAppPath.Length).Replace("\\", "/")));
                    }
                    var i = 0;
                    while (i < includedScripts.Count)
                    {
                        var scriptName = includedScripts[i];
                        if (scriptName.EndsWith(".min.js"))
                        {
                            if (AquariumExtenderBase.EnableMinifiedScript)
                            	scriptName = (scriptName.Substring(0, (scriptName.Length - 7)) + ".js");
                            includedScripts.Remove(scriptName);
                        }
                        else
                        	i++;
                    }
                }
                HttpRuntime.Cache.Add("IncludedScripts", includedScripts, dep, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            }
            foreach (var file in includedScripts)
            	scripts.Add(AquariumExtenderBase.CreateScriptReference(file));
        }
        
        Regex BuildSearchPathRegex(string[] paths)
        {
            if (paths.Length == 0)
            	return null;
            var sb = new StringBuilder();
            foreach (var path in paths)
            {
                if (sb.Length != 0)
                	sb.Append("|");
                sb.AppendFormat("({0})", Regex.Escape(path.Trim().Replace("/", "\\")).Replace("\\*", ".*"));
            }
            return new Regex(sb.ToString());
        }
        
        public static void CompressOutput(HttpContext context, string data)
        {
            var request = context.Request;
            var response = context.Response;
            var acceptEncoding = request.Headers["Accept-Encoding"];
            if (!(string.IsNullOrEmpty(acceptEncoding)))
            	if (acceptEncoding.Contains("gzip"))
                {
                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                    response.AppendHeader("Content-Encoding", "gzip");
                }
                else
                	if (acceptEncoding.Contains("deflate"))
                    {
                        response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                        response.AppendHeader("Content-Encoding", "deflate");
                    }
            var output = Encoding.UTF8.GetBytes(data);
            response.ContentEncoding = Encoding.Unicode;
            response.AddHeader("Content-Length", output.Length.ToString());
            response.OutputStream.Write(output, 0, output.Length);
            try
            {
                response.Flush();
            }
            catch (Exception)
            {
            }
        }
        
        public static void HandleServiceRequest(HttpContext context)
        {
            var methodName = context.Request.AppRelativeCurrentExecutionFilePath.ToLowerInvariant();
            if (methodName.StartsWith(AquariumExtenderBase.DefaultServicePath))
            	methodName = methodName.Substring((AquariumExtenderBase.DefaultServicePath.Length + 1));
            else
            	if (methodName.StartsWith(AquariumExtenderBase.AppServicePath))
                	methodName = methodName.Substring((AquariumExtenderBase.AppServicePath.Length + 1));
            if (string.IsNullOrEmpty(methodName))
            	throw new HttpException(400, "Method not specified.");
            ServiceRequestHandler handler = null;
            if (RequestHandlers.TryGetValue(methodName.ToLower(), out handler))
            {
                var args = RequestValidationService.ToJson(context);
                object result = null;
                if ((handler.AllowedMethods != null) && !(handler.AllowedMethods.Contains(context.Request.HttpMethod)))
                	throw new HttpException(405, "This HTTP Method is not allowed.");
                if (handler.RequiresAuthentication && !context.Request.IsAuthenticated)
                	throw new HttpException(403, "Requires authentication.");
                try
                {
                    result = handler.HandleRequest(new DataControllerService(), args);
                }
                catch (ServiceRequestRedirectException rex)
                {
                    result = new JObject();
                    ((JObject)(result))["RedirectUrl"] = rex.RedirectUrl;
                }
                catch (Exception ex)
                {
                    result = handler.HandleException(args, ex);
                }
                if (result != null)
                {
                    context.Response.ContentType = "application/json; charset=utf-8";
                    var output = string.Format("{{\"d\":{0}}}", JsonConvert.SerializeObject(result));
                    ApplicationServices.CompressOutput(context, CompressViewPageJsonOutput(output));
                }
            }
            else
            	throw new HttpException(404, "Method not found.");
            context.Response.End();
        }
        
        public static string CompressViewPageJsonOutput(string output)
        {
            var startIndex = 0;
            var dataIndex = 0;
            var lastIndex = 0;
            var lastLength = output.Length;
            while (true)
            {
                startIndex = output.IndexOf("{\"Controller\":", lastIndex, StringComparison.Ordinal);
                dataIndex = output.IndexOf(",\"NewRow\":", lastIndex, StringComparison.Ordinal);
                if ((startIndex < 0) || (dataIndex < 0))
                	break;
                var metadata = (output.Substring(0, startIndex) + ViewPageCompressRegex.Replace(output.Substring(startIndex, (dataIndex - startIndex)), string.Empty));
                if (metadata.EndsWith(","))
                	metadata = metadata.Substring(0, (metadata.Length - 1));
                output = (ViewPageCompress2Regex.Replace(metadata, "}$1") + output.Substring(dataIndex));
                lastIndex = ((dataIndex + 10) 
                            - (lastLength - output.Length));
                lastLength = output.Length;
            }
            return output;
        }
        
        public static string ResolveClientUrl(string relativeUrl)
        {
            var request = HttpContext.Current.Request;
            var root = (request.Url.Scheme 
                        + (Uri.SchemeDelimiter + request.Url.Host));
            if (!request.Url.IsDefaultPort)
            	root = (root 
                            + (":" + Convert.ToString(request.Url.Port)));
            if (relativeUrl.StartsWith("~/"))
            	relativeUrl = relativeUrl.Substring(2);
            else
            	if (relativeUrl.StartsWith("/"))
                	relativeUrl = relativeUrl.Substring(1);
                else
                	relativeUrl = (request.Url.AbsolutePath 
                                + ("/" + relativeUrl));
            var appPath = request.ApplicationPath;
            if (!(appPath.EndsWith("/")))
            	appPath = (appPath + "/");
            var result = ((root + appPath) 
                        + relativeUrl);
            if (string.IsNullOrEmpty(relativeUrl))
            	result = result.Substring(0, (result.Length - 1));
            return result;
        }
        
        public virtual SortedDictionary<string, string> CorsConfiguration(HttpRequest request)
        {
            if (EnableCors)
            {
                var list = new SortedDictionary<string, string>();
                var origin = request.Headers["Origin"];
                if (string.IsNullOrEmpty(origin))
                	origin = "*";
                list["Access-Control-Allow-Origin"] = origin;
                list["Access-Control-Allow-Methods"] = "GET,POST";
                list["Access-Control-Allow-Credentials"] = "true";
                list["Access-Control-Allow-Headers"] = "content-type,authorization";
                return list;
            }
            return null;
        }
        
        private static void EnsureJsonProperty(JObject ptr, string path, object defaultValue)
        {
            if (defaultValue == null)
            	defaultValue = string.Empty;
            var parts = path.Split('.');
            var counter = parts.Length;
            foreach (var part in parts)
            {
                counter--;
                if (ptr[part] == null)
                	if (counter != 0)
                    	ptr[part] = new JObject();
                    else
                    	ptr[part] = JToken.FromObject(defaultValue);
                if (counter != 0)
                	ptr = ((JObject)(ptr[part]));
            }
        }
        
        public static JToken TryGetJsonProperty(JObject ptr, string path)
        {
            var parts = path.Split('.');
            JToken temp = null;
            for (var i = 0; (i < (parts.Length - 1)); i++)
            {
                temp = ptr[parts[i]];
                if (temp != null)
                	ptr = ((JObject)(temp));
                else
                	return null;
            }
            return ptr[parts[(parts.Length - 1)]];
        }
        
        public virtual void OAuthSetState(string name, string value)
        {
        }
        
        public virtual void OAuthSetUserObject(JObject user)
        {
        }
        
        public virtual void OAuthSyncUser(MembershipUser user)
        {
        }
        
        public virtual bool ValidateBlobAccess(HttpContext context, BlobHandlerInfo handler, BlobAdapter ba, string val)
        {
            if (Blob.DirectAccessMode)
            	return true;
            var key = context.Request.Params["_validationKey"];
            if (((ba == null) || !ba.IsPublic) && (!context.User.Identity.IsAuthenticated && key != BlobAdapter.ValidationKey))
            	return !ApplicationServicesBase.AuthorizationIsSupported;
            var pr = new PageRequest(0, 1, string.Empty, null);
            var config = Controller.CreateConfigurationInstance(GetType(), handler.DataController);
            var iterator = config.Select("/c:dataController/c:fields/c:field[@isPrimaryKey=\'true\']");
            var filter = new List<string>();
            var vals = val.Split('|');
            var count = 0;
            var fieldFilter = new List<string>();
            while (iterator.MoveNext())
            {
                var pk = iterator.Current.GetAttribute("name", string.Empty);
                filter.Add(string.Format("{0}:={1}", pk, vals[count]));
                fieldFilter.Add(pk);
                count++;
            }
            pr.Filter = filter.ToArray();
            var fieldName = config.SelectSingleNode("/c:dataController/c:fields/c:field[@onDemandHandler=\'{0}\']/@name", handler.Key).Value;
            var view = string.Empty;
            iterator = config.Select(string.Format("/c:dataController/c:views/c:view[c:dataFields/c:dataField/@fieldName=\'{0}\' or c:c" +
                        "ategories/c:category/c:dataFields/c:dataField/@fieldName=\'{0}\']", fieldName), string.Empty);
            if (iterator.MoveNext())
            	view = iterator.Current.GetAttribute("id", string.Empty);
            else
            	view = Controller.GetSelectView(handler.DataController);
            fieldFilter.Add(fieldName);
            pr.FieldFilter = fieldFilter.ToArray();
            var page = ControllerFactory.CreateDataController().GetPage(handler.DataController, view, pr);
            // make sure that exactly one row is returned and the number of fields in the output is exactly equal to the number of PK fields plus 1 (blob field)
            if ((page.Rows.Count == 0) || !((page.Rows[0].Length == fieldFilter.Count)))
            	return false;
            return true;
        }
    }
    
    public class AnonymousUserIdentity : IIdentity
    {
        
        string IIdentity.AuthenticationType
        {
            get
            {
                return "None";
            }
        }
        
        bool IIdentity.IsAuthenticated
        {
            get
            {
                return false;
            }
        }
        
        string IIdentity.Name
        {
            get
            {
                return string.Empty;
            }
        }
    }
    
    public partial class ApplicationSiteMapProvider : ApplicationSiteMapProviderBase
    {
    }
    
    public class ApplicationSiteMapProviderBase : System.Web.XmlSiteMapProvider
    {
        
        public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
        {
            var device = node["Device"];
            var isTouchUI = ApplicationServices.IsTouchClient;
            if ((device == "touch") && !isTouchUI)
            	return false;
            if ((device == "desktop") && isTouchUI)
            	return false;
            return base.IsAccessibleToUser(context, node);
        }
    }
    
    public partial class PlaceholderHandler : PlaceholderHandlerBase
    {
    }
    
    public class PlaceholderHandlerBase : IHttpHandler
    {
        
        private static Regex _imageSizeRegex = new Regex("((?\'background\'[a-zA-Z0-9]+?)-((?\'textcolor\'[a-zA-Z0-9]+?)-)?)?(?\'width\'[0-9]+?)(" +
                "x(?\'height\'[0-9]*))?\\.[a-zA-Z][a-zA-Z][a-zA-Z]");
        
        bool IHttpHandler.IsReusable
        {
            get
            {
                return true;
            }
        }
        
        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            // Get file name
            var routeValues = context.Request.RequestContext.RouteData.Values;
            var fileName = ((string)(routeValues["FileName"]));
            // Get extension
            var ext = Path.GetExtension(fileName);
            var format = ImageFormat.Png;
            var contentType = "image/png";
            if (ext == ".jpg")
            {
                format = ImageFormat.Jpeg;
                contentType = "image/jpg";
            }
            else
            	if (ext == ".gif")
                {
                    format = ImageFormat.Gif;
                    contentType = "image/jpg";
                }
            // get width and height
            var regexMatch = _imageSizeRegex.Matches(fileName)[0];
            var widthCapture = regexMatch.Groups["width"];
            var width = 500;
            if (widthCapture.Length != 0)
            	width = Convert.ToInt32(widthCapture.Value);
            if (width == 0)
            	width = 500;
            if (width > 4096)
            	width = 4096;
            var heightCapture = regexMatch.Groups["height"];
            var height = width;
            if (heightCapture.Length != 0)
            	height = Convert.ToInt32(heightCapture.Value);
            if (height == 0)
            	height = 500;
            if (height > 4096)
            	height = 4096;
            // Get background and text colors
            var background = GetColor(regexMatch.Groups["background"], Color.LightGray);
            var textColor = GetColor(regexMatch.Groups["textcolor"], Color.Black);
            var fontSize = ((width + height) 
                        / 50);
            if (fontSize < 10)
            	fontSize = 10;
            var font = new Font(FontFamily.GenericSansSerif, fontSize);
            // Get text
            var text = context.Request.QueryString["text"];
            if (string.IsNullOrEmpty(text))
            	text = string.Format("{0} x {1}", width, height);
            // Get position for text
            SizeF textSize;
            using (var img = new Bitmap(1, 1))
            {
                var textDrawing = Graphics.FromImage(img);
                textSize = textDrawing.MeasureString(text, font);
            }
            // Draw the image
            using (var image = new Bitmap(width, height))
            {
                var drawing = Graphics.FromImage(image);
                drawing.Clear(background);
                using (var textBrush = new SolidBrush(textColor))
                	drawing.DrawString(text, font, textBrush, ((width - textSize.Width) 
                                    / 2), ((height - textSize.Height) 
                                    / 2));
                drawing.Save();
                drawing.Dispose();
                // Return image
                using (var stream = new MemoryStream())
                {
                    image.Save(stream, format);
                    var cache = context.Response.Cache;
                    cache.SetCacheability(HttpCacheability.Public);
                    cache.SetOmitVaryStar(true);
                    cache.SetExpires(DateTime.Now.AddDays(365));
                    cache.SetValidUntilExpires(true);
                    cache.SetLastModifiedFromFileDependencies();
                    context.Response.ContentType = contentType;
                    context.Response.AddHeader("Content-Length", Convert.ToString(stream.Length));
                    context.Response.AddHeader("File-Name", fileName);
                    context.Response.BinaryWrite(stream.ToArray());
                    context.Response.OutputStream.Flush();
                }
            }
        }
        
        private static Color GetColor(Capture colorName, Color defaultColor)
        {
            try
            {
                if (colorName.Length > 0)
                {
                    var s = colorName.Value;
                    if (Regex.IsMatch(s, "^[0-9abcdef]{3,6}$"))
                    	s = ("#" + s);
                    return ColorTranslator.FromHtml(s);
                }
            }
            catch (Exception)
            {
            }
            return defaultColor;
        }
    }
    
    public class GenericRoute : IRouteHandler
    {
        
        private IHttpHandler _handler;
        
        public GenericRoute(IHttpHandler handler)
        {
            _handler = handler;
        }
        
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext context)
        {
            return _handler;
        }
        
        public static void Map(RouteCollection routes, IHttpHandler handler, string url)
        {
            var r = new Route(url, new GenericRoute(handler));
            r.Defaults = new RouteValueDictionary();
            r.Constraints = new RouteValueDictionary();
            routes.Add(r);
        }
    }
    
    public class SaasConfiguration
    {
        
        private string _config;
        
        private string _clientId;
        
        private string _clientSecret;
        
        private string _redirectUri;
        
        private string _accessToken;
        
        private string _refreshToken;
        
        public SaasConfiguration(string config)
        {
            _config = (("\n" + config) 
                        + "\n");
        }
        
        public virtual string ClientId
        {
            get
            {
                if (string.IsNullOrEmpty(_clientId))
                	_clientId = this["Client Id"];
                return _clientId;
            }
        }
        
        public virtual string ClientSecret
        {
            get
            {
                if (string.IsNullOrEmpty(_clientSecret))
                	_clientSecret = this["Client Secret"];
                return _clientSecret;
            }
        }
        
        public virtual string RedirectUri
        {
            get
            {
                if (HttpContext.Current.Request.IsLocal && string.IsNullOrEmpty(_redirectUri))
                	_redirectUri = this["Local Redirect Uri"];
                if (string.IsNullOrEmpty(_redirectUri))
                	_redirectUri = this["Redirect Uri"];
                return _redirectUri;
            }
        }
        
        public virtual string AccessToken
        {
            get
            {
                if (string.IsNullOrEmpty(_accessToken))
                	_accessToken = this["Access Token"];
                return _accessToken;
            }
            set
            {
                _accessToken = value;
                this["Access Token"] = value;
            }
        }
        
        public virtual string RefreshToken
        {
            get
            {
                if (string.IsNullOrEmpty(_refreshToken))
                	_refreshToken = this["Refresh Token"];
                return _refreshToken;
            }
            set
            {
                _refreshToken = value;
                this["Refresh Token"] = value;
            }
        }
        
        public virtual string this[string property]
        {
            get
            {
                if (string.IsNullOrEmpty(_config))
                	return string.Empty;
                var m = Regex.Match(_config, (("\\n(" + property) 
                                + ")\\:\\s*?\\n?(?\'Value\'[^\\s\\n].+?)\\n"), RegexOptions.IgnoreCase);
                if (m.Success)
                	return m.Groups["Value"].Value.Trim();
                return string.Empty;
            }
            set
            {
                if (!(string.IsNullOrEmpty(_config)))
                {
                    var oldValue = this[property];
                    if (!(string.IsNullOrEmpty(oldValue)))
                    	_config = _config.Replace(oldValue, value);
                    else
                    	_config = (_config 
                                    + ((Environment.NewLine + property) 
                                    + (": " + value)));
                }
            }
        }
        
        public override string ToString()
        {
            return _config;
        }
    }
    
    public abstract class OAuthHandler
    {
        
        public string StartPage;
        
        private bool _refreshedToken = false;
        
        private SiteContentFile _saasFile;
        
        private string _clientUri;
        
        private SaasConfiguration _config = null;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private JObject _tokens;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool _storeToken;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _appState;
        
        public virtual string ClientUri
        {
            get
            {
                if (string.IsNullOrEmpty(_clientUri) && ApplicationServices.IsSiteContentEnabled)
                {
                    _clientUri = Config["Client Uri"];
                    if (!(_clientUri.StartsWith("http")))
                    	_clientUri = ("https://" + _clientUri);
                }
                return _clientUri;
            }
        }
        
        protected virtual SaasConfiguration Config
        {
            get
            {
                if ((_config == null) && ApplicationServices.IsSiteContentEnabled)
                {
                    _saasFile = ApplicationServices.Current.ReadSiteContent(("sys/saas/" + GetHandlerName().ToLower()));
                    if (_saasFile != null)
                    	_config = new SaasConfiguration(_saasFile.Text);
                }
                return _config;
            }
        }
        
        protected virtual JObject Tokens
        {
            get
            {
                return this._tokens;
            }
            set
            {
                this._tokens = value;
            }
        }
        
        protected virtual bool StoreToken
        {
            get
            {
                return this._storeToken;
            }
            set
            {
                this._storeToken = value;
            }
        }
        
        protected virtual string Scope
        {
            get
            {
                return string.Empty;
            }
        }
        
        public string AppState
        {
            get
            {
                return this._appState;
            }
            set
            {
                this._appState = value;
            }
        }
        
        public virtual void ProcessRequest(HttpContext context)
        {
            try
            {
                var services = ApplicationServices.Create();
                StartPage = context.Request.QueryString["start"];
                if (string.IsNullOrEmpty(StartPage))
                	StartPage = services.UserHomePageUrl();
                var state = context.Request.QueryString["state"];
                if (!(string.IsNullOrEmpty(state)))
                	SetState(state);
                RestoreSession(context);
                if (Config == null)
                	throw new Exception("Provider not found.");
                else
                {
                    var code = GetAuthCode(context.Request);
                    if (string.IsNullOrEmpty(code))
                    {
                        var er = context.Request.QueryString["error"];
                        if (!(string.IsNullOrEmpty(er)))
                        	HandleError(context);
                        else
                        	context.Response.Redirect(GetAuthorizationUrl());
                    }
                    else
                    	if (!(GetAccessTokens(code, false)))
                        	context.Response.StatusCode = 401;
                        else
                        {
                            StoreTokens(Tokens, StoreToken);
                            SetSession(context);
                            RedirectToStartPage(context);
                        }
                }
            }
            catch (Exception ex)
            {
                HandleException(context, ex);
            }
        }
        
        public virtual void SetSession(HttpContext context)
        {
            if (!StoreToken)
            {
                var user = SyncUser();
                if (user == null)
                	throw new Exception("No user found.");
                var services = ApplicationServices.Current;
                // logout current user
                var auth = context.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (auth != null)
                {
                    var oldTicket = FormsAuthentication.Decrypt(auth.Value);
                    if (oldTicket.Name != user.UserName)
                    	services.UserLogout();
                }
                var ticket = new FormsAuthenticationTicket(0, user.UserName, DateTime.Now, DateTime.Now.AddHours(12), false, ("OAUTH:" + GetHandlerName()));
                var encrypted = FormsAuthentication.Encrypt(ticket);
                var accountManagerEnabled = ApplicationServices.TryGetJsonProperty(services.DefaultSettings, "membership.accountManager.enabled");
                if ((accountManagerEnabled == null) || accountManagerEnabled.Value<bool>())
                {
                    // client token login
                    var cookie = new HttpCookie(".TOKEN", encrypted);
                    cookie.Expires = System.DateTime.Now.AddMinutes(5);
                    context.Response.SetCookie(cookie);
                }
                else
                {
                    // server login
                    services.AuthenticateUser(user.UserName, ("token:" + encrypted), false);
                }
                context.Response.Cookies.Set(new HttpCookie(".PROVIDER", GetHandlerName()));
            }
        }
        
        public virtual void RestoreSession(HttpContext context)
        {
            if (context.Request.QueryString["storeToken"] == "true")
            	StoreToken = true;
        }
        
        protected virtual bool GetAccessTokens(string code, bool refresh)
        {
            var request = GetAccessTokenRequest(code, refresh);
            var response = request.GetResponse();
            var json = string.Empty;
            using (var sr = new StreamReader(response.GetResponseStream()))
            	json = sr.ReadToEnd();
            if (!HttpContext.Current.IsCustomErrorEnabled && (string.IsNullOrEmpty(json) || !((json[0] == '{'))))
            	throw new Exception(("Error fetching access tokens. Response: " + json));
            var responseObj = JObject.Parse(json);
            var er = ((string)(responseObj["error"]));
            if (!(string.IsNullOrEmpty(er)))
            	throw new Exception(er);
            Tokens = responseObj;
            return (responseObj["access_token"] != null);
        }
        
        protected virtual void StoreTokens(JObject tokens, bool storeSystem)
        {
            if (storeSystem && (ApplicationServices.IsSiteContentEnabled && (_config != null)))
            {
                _config.AccessToken = ((string)(tokens["access_token"]));
                _config.RefreshToken = ((string)(tokens["refresh_token"]));
                _saasFile.Text = _config.ToString().Trim();
                SiteContentFile.WriteAllText(string.Format("{0}/{1}", _saasFile.Path, _saasFile.Name), _saasFile.Text);
            }
        }
        
        public virtual bool LoadTokens(string userName)
        {
            return false;
        }
        
        protected virtual string GetAuthCode(HttpRequest request)
        {
            return request.QueryString["code"];
        }
        
        public virtual JObject Query(string method, bool useSystemToken)
        {
            JObject result = null;
            try
            {
                var token = ((string)(Tokens["access_token"]));
                if (useSystemToken)
                	token = Config.AccessToken;
                if (string.IsNullOrEmpty(token))
                	throw new Exception("No token for request.");
                var request = GetQueryRequest(method, token);
                var response = request.GetResponse();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    result = JObject.Parse(sr.ReadToEnd());
                    ApplicationServicesBase.Create().OAuthSetUserObject(result);
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ((HttpWebResponse)(ex.Response));
                    if ((response.StatusCode == HttpStatusCode.Unauthorized) && !_refreshedToken)
                    {
                        _refreshedToken = true;
                        if (!(RefreshTokens(useSystemToken)))
                        	throw new Exception("Token expired.");
                        else
                        	result = Query(method, useSystemToken);
                    }
                    else
                    	if (response.StatusCode == HttpStatusCode.Forbidden)
                        	throw new Exception("Insufficient permissions.");
                }
            }
            return result;
        }
        
        protected virtual bool RefreshTokens(bool useSystemToken)
        {
            var refresh = ((string)(Tokens["refresh_token"]));
            if (useSystemToken)
            	refresh = Config.RefreshToken;
            if (!(string.IsNullOrEmpty(refresh)))
            {
                if (GetAccessTokens(refresh, true))
                {
                    if (useSystemToken)
                    	StoreTokens(Tokens, true);
                    return true;
                }
            }
            return false;
        }
        
        public virtual MembershipUser SyncUser()
        {
            var username = GetUserName();
            var email = GetUserEmail();
            var user = Membership.GetUser(username);
            if (user == null)
            {
                var userNameOfEmailOwner = Membership.GetUserNameByEmail(username);
                if (!(string.IsNullOrEmpty(userNameOfEmailOwner)))
                	user = Membership.GetUser(userNameOfEmailOwner);
            }
            if ((user == null) && (Config["Sync User"] == "true"))
            {
                // create user
                var comment = ("Source: " + GetHandlerName());
                MembershipCreateStatus status;
                if (string.IsNullOrEmpty(email))
                	email = username;
                user = Membership.CreateUser(username, Guid.NewGuid().ToString(), email, comment, Guid.NewGuid().ToString(), true, out status);
                if (status != MembershipCreateStatus.Success)
                	throw new Exception(status.ToString());
                user.Comment = comment;
                Membership.UpdateUser(user);
                Roles.AddUserToRoles(user.UserName, GetDefaultUserRoles(user));
            }
            if (user != null)
            {
                if (!(string.IsNullOrEmpty(email)) && email != user.Email)
                {
                    user.Email = email;
                    Membership.UpdateUser(user);
                }
                SetUserAvatar(user);
                if (Config["Sync Roles"] == "true")
                {
                    // verify roles
                    var roleList = GetUserRoles(user);
                    foreach (var role in roleList)
                    	if (!(Roles.IsUserInRole(user.UserName, role)))
                        {
                            if (!(Roles.RoleExists(role)))
                            	Roles.CreateRole(role);
                            Roles.AddUserToRole(user.UserName, role);
                        }
                    var existingRoles = new List<string>(Roles.GetRolesForUser(user.UserName));
                    foreach (var oldRole in existingRoles)
                    	if (!(roleList.Contains(oldRole)))
                        	Roles.RemoveUserFromRole(user.UserName, oldRole);
                }
            }
            ApplicationServicesBase.Create().OAuthSyncUser(user);
            return user;
        }
        
        public abstract string GetUserName();
        
        public virtual string GetUserEmail()
        {
            return string.Empty;
        }
        
        public virtual void SetUserAvatar(MembershipUser user)
        {
            if (ApplicationServices.IsSiteContentEnabled)
            	try
                {
                    var url = GetUserImageUrl(user);
                    if (!(string.IsNullOrEmpty(url)))
                    {
                        var request = WebRequest.Create(url);
                        var response = request.GetResponse();
                        using (var s = response.GetResponseStream())
                        {
                            var b = ((Bitmap)(Bitmap.FromStream(s)));
                            using (var ms = new MemoryStream())
                            {
                                b.Save(ms, ImageFormat.Png);
                                SiteContentFile.WriteAllBytes(string.Format("sys/users/{0}.png", user.UserName), "image/png", ms.ToArray());
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
        }
        
        public virtual string GetUserImageUrl(MembershipUser user)
        {
            return null;
        }
        
        public virtual string[] GetDefaultUserRoles(MembershipUser user)
        {
            return new string[] {
                    "Users"};
        }
        
        public virtual List<string> GetUserRoles(MembershipUser user)
        {
            var roleList = new List<string>();
            roleList.Add("Users");
            return roleList;
        }
        
        public virtual string GetUserProfile()
        {
            return "logout";
        }
        
        public virtual string GetState()
        {
            var state = ("start=" + StartPage);
            if (StoreToken)
            	state = (state + "|storeToken=true");
            if (!(string.IsNullOrEmpty(AppState)))
            	state = (state 
                            + ("|" + AppState));
            return state;
        }
        
        public virtual void SetState(string state)
        {
            foreach (var part in state.Split('|'))
            	if (!(string.IsNullOrEmpty(part)))
                {
                    var ps = part.Split('=');
                    if (ps[0] == "start")
                    	StartPage = ps[1];
                    else
                    	if (ps[0] == "storeToken")
                        	StoreToken = ((ps[1] == "true") && ApplicationServicesBase.IsSuperUser);
                        else
                        	ApplicationServicesBase.Create().OAuthSetState(ps[0], ps[1]);
                }
        }
        
        public virtual void RedirectToLoginPage()
        {
            string redirectUrl = null;
            if (Config == null)
            	redirectUrl = ApplicationServices.Create().UserHomePageUrl();
            else
            	redirectUrl = GetAuthorizationUrl();
            HttpContext.Current.Response.Redirect(redirectUrl);
        }
        
        public virtual void RedirectToStartPage(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            	context.Response.Redirect(StartPage);
            else
            	context.Response.Redirect(((ApplicationServices.Current.UserHomePageUrl() + "?ReturnUrl=") 
                                + HttpUtility.UrlEncode(ApplicationServices.ResolveClientUrl(StartPage))));
        }
        
        public virtual bool ValidateRefreshToken(MembershipUser user, string token)
        {
            return true;
        }
        
        public virtual void SignOut()
        {
        }
        
        protected virtual void HandleError(HttpContext context)
        {
            var desc = context.Request.QueryString["error_description"];
            if (string.IsNullOrEmpty(desc))
            	desc = context.Request.QueryString["error"];
            throw new Exception(desc);
        }
        
        protected virtual void HandleException(HttpContext context, Exception ex)
        {
            while (ex.InnerException != null)
            	ex = ex.InnerException;
            var er = new ServiceRequestError();
            er.Message = ex.Message;
            er.ExceptionType = ex.GetType().ToString();
            if (!context.IsCustomErrorEnabled)
            	er.StackTrace = ex.StackTrace;
            context.Server.ClearError();
            context.Response.TrySkipIisCustomErrors = true;
            context.Response.ContentType = "application/json";
            context.Response.Clear();
            context.Response.Write(JsonConvert.SerializeObject(er));
        }
        
        public abstract string GetHandlerName();
        
        public abstract string GetAuthorizationUrl();
        
        protected abstract WebRequest GetAccessTokenRequest(string code, bool refresh);
        
        protected abstract WebRequest GetQueryRequest(string method, string token);
    }
    
    public partial class OAuthHandlerFactory : OAuthHandlerFactoryBase
    {
    }
    
    public class OAuthHandlerFactoryBase
    {
        
        public static SortedDictionary<string, Type> Handlers = new SortedDictionary<string, Type>();
        
        public static OAuthHandler Create(string service)
        {
            return new OAuthHandlerFactory().GetHandler(service);
        }
        
        public static OAuthHandler GetActiveHandler()
        {
            var saas = HttpContext.Current.Request.Cookies[".PROVIDER"];
            if ((saas != null) && (saas.Value != null))
            	return OAuthHandlerFactory.Create(saas.Value);
            return null;
        }
        
        public virtual OAuthHandler GetHandler(string service)
        {
            Type t = null;
            if (Handlers.TryGetValue(service.ToLower(), out t))
            	return ((OAuthHandler)(Activator.CreateInstance(t)));
            return null;
        }
        
        public static OAuthHandler CreateAutoLogin()
        {
            return new OAuthHandlerFactory().GetAutoLoginHandler();
        }
        
        public virtual OAuthHandler GetAutoLoginHandler()
        {
            if (ApplicationServices.IsSiteContentEnabled)
            	foreach (var file in ApplicationServices.Create().ReadSiteContent("sys/saas", "%"))
                	if (!(string.IsNullOrEmpty(file.Text)) && Regex.IsMatch(file.Text, "Auto Login:\\s*true"))
                    	return GetHandler(file.Name);
            return null;
        }
    }
    
    public partial class CloudIdentityOAuthHandler : CloudIdentityOAuthHandlerBase
    {
    }
    
    public partial class CloudIdentityOAuthHandlerBase : OAuthHandler
    {
        
        private JObject _userObj;
        
        protected override string Scope
        {
            get
            {
                var scopes = Config["Scope"];
                if (string.IsNullOrEmpty(scopes))
                	scopes = "profile email";
                return scopes;
            }
        }
        
        public override string GetHandlerName()
        {
            return "CloudIdentity";
        }
        
        public override string GetAuthorizationUrl()
        {
            return string.Format("{0}/oauth/auth?response_type=code&client_id={1}&redirect_uri={2}&scope={3}&state=" +
                    "{4}", ClientUri, Config.ClientId, Uri.EscapeDataString(Config.RedirectUri), Uri.EscapeDataString(Scope), Uri.EscapeDataString(GetState()));
        }
        
        protected override WebRequest GetAccessTokenRequest(string code, bool refresh)
        {
            var request = WebRequest.Create((ClientUri + "/oauth/token"));
            request.Method = "POST";
            var codeType = "code";
            if (refresh)
            	codeType = "access_token";
            var body = string.Format("{0}={1}&client_id={2}&client_secret={3}&redirect_uri={4}&grant_type=authorization" +
                    "_code", codeType, code, Config.ClientId, Config.ClientSecret, Config.RedirectUri);
            var bodyBytes = Encoding.UTF8.GetBytes(body);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bodyBytes.Length;
            using (var stream = request.GetRequestStream())
            	stream.Write(bodyBytes, 0, bodyBytes.Length);
            return request;
        }
        
        protected override WebRequest GetQueryRequest(string method, string token)
        {
            var request = WebRequest.Create((ClientUri 
                            + ("/oauth/" + method)));
            request.Headers[HttpRequestHeader.Authorization] = ("Bearer " + token);
            return request;
        }
        
        public override string GetUserName()
        {
            _userObj = Query("user", false);
            return ((string)(_userObj["name"]));
        }
        
        public override MembershipUser SyncUser()
        {
            var user = base.SyncUser();
            if (ApplicationServices.IsSiteContentEnabled && (user != null))
            	SiteContentFile.WriteJson(string.Format("sys/users/{0}.json", user.UserName), _userObj);
            return user;
        }
        
        public override bool LoadTokens(string userName)
        {
            if (ApplicationServices.IsSiteContentEnabled)
            {
                _userObj = SiteContentFile.ReadJson(string.Format("sys/users/{0}.json", userName));
                if (_userObj.HasValues)
                {
                    Tokens = _userObj;
                    return true;
                }
            }
            return false;
        }
        
        public override bool ValidateRefreshToken(MembershipUser user, string token)
        {
            if (!ApplicationServices.IsSiteContentEnabled)
            	return true;
            if (LoadTokens(user.UserName))
            {
                var response = Query("user", false);
                if ((response != null) && (((string)(response["name"])) == user.UserName))
                	return true;
            }
            return false;
        }
    }
    
    public partial class DnnOAuthHandler : DnnOAuthHandlerBase
    {
    }
    
    public partial class DnnOAuthHandlerBase : OAuthHandler
    {
        
        private string _showNavigation;
        
        private JObject _userInfo;
        
        protected override string Scope
        {
            get
            {
                var sc = Config["Scope"];
                var tokens = Config["Tokens"];
                if (!(string.IsNullOrEmpty(tokens)))
                	sc = (sc 
                                + (" token:" + string.Join(" token:", tokens.Split(' '))));
                return sc;
            }
        }
        
        public override string GetHandlerName()
        {
            return "DNN";
        }
        
        public override string GetAuthorizationUrl()
        {
            var authUrl = string.Format("{0}?response_type=code&client_id={1}&redirect_uri={2}&state={3}", ClientUri, Config.ClientId, Config.RedirectUri, Uri.EscapeDataString(GetState()));
            if (!(string.IsNullOrEmpty(Scope)))
            	authUrl = (authUrl 
                            + ("&scope=" + Uri.EscapeDataString(Scope)));
            var username = HttpContext.Current.Request.QueryString["username"];
            if (!(string.IsNullOrEmpty(username)))
            	authUrl = (authUrl 
                            + ("&username=" + username));
            return authUrl;
        }
        
        protected override WebRequest GetAccessTokenRequest(string code, bool refresh)
        {
            var request = WebRequest.Create(ClientUri);
            request.Method = "POST";
            var codeType = "code";
            if (refresh)
            	codeType = "access_token";
            var body = string.Format("{0}={1}&client_id={2}&client_secret={3}&redirect_uri={4}&grant_type=authorization" +
                    "_code", codeType, code, Config.ClientId, Config.ClientSecret, Uri.EscapeDataString(Config.RedirectUri));
            var bodyBytes = Encoding.UTF8.GetBytes(body);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bodyBytes.Length;
            using (var stream = request.GetRequestStream())
            	stream.Write(bodyBytes, 0, bodyBytes.Length);
            return request;
        }
        
        protected override WebRequest GetQueryRequest(string method, string token)
        {
            var request = WebRequest.Create((ClientUri 
                            + ("?method=" + method)));
            request.Headers[HttpRequestHeader.Authorization] = ("Bearer " + token);
            return request;
        }
        
        public override string GetState()
        {
            return (base.GetState() 
                        + ("|showNavigation=" + HttpContext.Current.Request.QueryString["showNavigation"]));
        }
        
        public override void SetState(string state)
        {
            base.SetState(state);
            foreach (var part in state.Split('|'))
            {
                var ps = part.Split('=');
                if (ps[0] == "showNavigation")
                	_showNavigation = ps[1];
            }
        }
        
        public override void RestoreSession(HttpContext context)
        {
            if (string.IsNullOrEmpty(_showNavigation))
            	_showNavigation = context.Request.QueryString["showNavigation"];
            var session = context.Request.QueryString["session"];
            if (!(string.IsNullOrEmpty(session)) && (session == "new"))
            	ApplicationServices.Current.UserLogout();
            else
            {
                base.RestoreSession(context);
                if (!StoreToken && context.User.Identity.IsAuthenticated)
                	RedirectToStartPage(context);
            }
        }
        
        public override void RedirectToStartPage(HttpContext context)
        {
            var connector = "?";
            if (StartPage.Contains("?"))
            	connector = "&";
            StartPage = (StartPage 
                        + (connector 
                        + ("_showNavigation=" + _showNavigation)));
            base.RedirectToStartPage(context);
        }
        
        public override string GetUserName()
        {
            return ((string)(_userInfo["UserName"]));
        }
        
        public override string GetUserEmail()
        {
            return ((string)(_userInfo["UserEmail"]));
        }
        
        public override List<string> GetUserRoles(MembershipUser user)
        {
            var roles = base.GetUserRoles(user);
            foreach (var r in _userInfo.Value<JArray>("Roles"))
            	roles.Add(r.ToString());
            return roles;
        }
        
        public override MembershipUser SyncUser()
        {
            _userInfo = Query("me", false);
            var user = base.SyncUser();
            SiteContentFile.WriteJson(string.Format("sys/users/{0}.json", user.UserName), ((JObject)(_userInfo["Tokens"])));
            return user;
        }
        
        public override string GetUserImageUrl(MembershipUser user)
        {
            return string.Format("{0}/DnnImageHandler.ashx?mode=profilepic&userId={1}&h=80&w=80", ClientUri, Convert.ToInt32(_userInfo["UserID"]));
        }
        
        public override void SignOut()
        {
            var url = ApplicationServices.ResolveClientUrl(ApplicationServices.Current.UserHomePageUrl());
            ServiceRequestHandler.Redirect(string.Format("{0}?_logout=true&client_id={1}&redirect_uri={2}", ClientUri, Config.ClientId, url));
        }
    }
    
    public class UserTicket
    {
        
        [JsonProperty("name")]
        public string UserName;
        
        [JsonProperty("email")]
        public string Email;
        
        [JsonProperty("access_token")]
        public string AccessToken;
        
        [JsonProperty("refresh_token")]
        public string RefreshToken;
        
        [JsonProperty("picture")]
        public string Picture;
        
        [JsonProperty("claims")]
        public Dictionary<string, string> Claims = new Dictionary<string, string>();
        
        public UserTicket()
        {
        }
        
        public UserTicket(MembershipUser user)
        {
            UserName = user.UserName;
            Email = user.Email;
            Picture = ApplicationServices.Create().UserPictureString(user);
        }
        
        public UserTicket(MembershipUser user, string accessToken, string refreshToken) : 
                this(user)
        {
            this.AccessToken = accessToken;
            this.RefreshToken = refreshToken;
        }
    }
    
    public class ManifestFile
    {
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _name;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _path;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _mD5;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int _length;
        
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
        
        public string Path
        {
            get
            {
                return this._path;
            }
            set
            {
                this._path = value;
            }
        }
        
        public string MD5
        {
            get
            {
                return this._mD5;
            }
            set
            {
                this._mD5 = value;
            }
        }
        
        public int Length
        {
            get
            {
                return this._length;
            }
            set
            {
                this._length = value;
            }
        }
        
        public static ManifestFile FromPath(string relativePath)
        {
            var f = new ManifestFile();
            if (relativePath.Contains("?"))
            	relativePath = relativePath.Substring(0, relativePath.IndexOf("?"));
            f.Path = relativePath.Replace('\\', '/').Replace("~/", string.Empty);
            f.Name = System.IO.Path.GetFileName(f.Path);
            byte[] fileBytes = null;
            if (f.Path == "js/daf/add.min.js")
            	fileBytes = Encoding.UTF8.GetBytes(ApplicationServices.Current.AddScripts());
            else
            	if (f.Path == "css/daf/add.min.css")
                	fileBytes = Encoding.UTF8.GetBytes(ApplicationServices.Current.AddStyleSheets());
                else
                	fileBytes = File.ReadAllBytes(HttpContext.Current.Server.MapPath(("~/" + f.Path)));
            f.MD5 = ComputeHash(fileBytes);
            f.Length = fileBytes.Length;
            return f;
        }
        
        public static ManifestFile FromResource(string resourceName)
        {
            var file = new ManifestFile();
            file.Name = resourceName;
            file.Path = ("_resources/" + resourceName);
            using (var s = ControllerConfigurationUtility.GetResourceStream(resourceName))
            	using (var ms = new MemoryStream())
                {
                    s.CopyTo(ms);
                    file.MD5 = ComputeHash(ms.ToArray());
                    file.Length = Convert.ToInt32(ms.Length);
                }
            return file;
        }
        
        public static ManifestFile FromUrl(string url)
        {
            url = url.Replace('\\', '/').Replace("~/", string.Empty);
            var file = new ManifestFile();
            file.Path = url;
            file.Name = System.IO.Path.GetFileName(file.Path);
            using (var sw = new StringWriter())
            {
                var handlerUrl = ("/" + url.Replace(".html", string.Empty).Replace(".htm", string.Empty));
                var page = GetContent(handlerUrl);
                var bytes = Encoding.UTF8.GetBytes(page);
                file.MD5 = ComputeHash(bytes);
                file.Length = bytes.Length;
            }
            return file;
        }
        
        public static ManifestFile GetConfig(string config)
        {
            var configBytes = Encoding.UTF8.GetBytes(config);
            var configFile = new ManifestFile();
            configFile.Path = "js/host/config.js";
            configFile.Name = "config.js";
            configFile.Length = configBytes.Length;
            configFile.MD5 = ComputeHash(configBytes);
            return configFile;
        }
        
        public static string ComputeHash(byte[] data)
        {
            var prov = new MD5CryptoServiceProvider();
            var hashData = prov.ComputeHash(data);
            var sb = new StringBuilder();
            for (var i = 0; (i < hashData.Length); i++)
            	sb.Append(hashData[i].ToString("x2"));
            return sb.ToString();
        }
        
        public static string GetContent(string url)
        {
            var original = HttpContext.Current.Request;
            var homePage = ApplicationServices.ResolveClientUrl(url);
            var request = WebRequest.CreateHttp(homePage);
            request.AutomaticDecompression = (DecompressionMethods.Deflate | DecompressionMethods.GZip);
            foreach (string header in original.Headers)
            	if (!(WebHeaderCollection.IsRestricted(header)) && header != "Cookie")
                	request.Headers[header] = original.Headers[header];
            request.Headers["X-Cot-Manifest-Request"] = "true";
            using (var sr = new StreamReader(request.GetResponse().GetResponseStream()))
            	return sr.ReadToEnd();
        }
    }
    
    public class StylesheetGenerator
    {
        
        private string _template;
        
        private JObject _theme;
        
        private JObject _accent;
        
        private SortedDictionary<string, string> _themeVariables = new SortedDictionary<string, string>();
        
        public static Regex ThemeStylesheetRegex = new Regex("^touch-theme\\.(?\'Theme\'\\w+)\\.((?\'Accent\'\\w+)\\.)?css$");
        
        public static Regex ThemeVariableRegex = new Regex("(?\'Item\'(?\'Before\'\\w+:\\s*)\\/\\*\\s*(?\'Name\'(@[\\w\\.]+(,\\s*)?)+)\\s*\\*\\/(?\'Value\'.+?))" +
                "(?\'After\'(!important)?;\\s*)$", RegexOptions.Multiline);
        
        public StylesheetGenerator(string theme, string accent)
        {
            var touchPath = HttpContext.Current.Server.MapPath("~/css");
            var css = Path.Combine(touchPath, "daf", "touch-theme.css");
            if (File.Exists(css))
            {
                _template = File.ReadAllText(css);
                var themeFile = Path.Combine(touchPath, "themes", ("touch-theme." 
                                + (theme + ".json")));
                var accentFile = Path.Combine(touchPath, "themes", ("touch-accent." 
                                + (accent + ".json")));
                if (File.Exists(themeFile) && File.Exists(accentFile))
                {
                    _accent = JObject.Parse(File.ReadAllText(accentFile));
                    _theme = JObject.Parse(File.ReadAllText(themeFile));
                }
            }
        }
        
        public static string Compile(string fileName)
        {
            var m = ThemeStylesheetRegex.Match(fileName);
            if (m.Success)
            	return new StylesheetGenerator(m.Groups["Theme"].Value, m.Groups["Accent"].Value).ToString();
            return string.Empty;
        }
        
        public static string Minify(string css)
        {
            css = Regex.Replace(css, "[a-zA-Z]+#", "#");
            css = Regex.Replace(css, "[\\n\\r]+\\s*", string.Empty);
            css = Regex.Replace(css, "\\s\\s+", " ");
            css = Regex.Replace(css, "\\s?([:,;{}])\\s?", "$1");
            css = css.Replace(";}", "}");
            css = Regex.Replace(css, "([\\s:]0)(px|pt|%|em)", "$1");
            css = Regex.Replace(css, "/\\*[\\d\\D]*?\\*/", string.Empty);
            return css;
        }
        
        public override string ToString()
        {
            var result = _template;
            if (!(string.IsNullOrEmpty(_template)) && ((_theme != null) && (_accent != null)))
            	result = ThemeVariableRegex.Replace(result, DoReplaceThemeVariables);
            if (ApplicationServices.EnableMinifiedCss)
            	result = Minify(result);
            return result;
        }
        
        protected string DoReplaceThemeVariables(Match m)
        {
            var variable = m.Groups["Name"].Value;
            var before = m.Groups["Before"].Value;
            var after = m.Groups["After"].Value;
            var parts = variable.Split(',');
            string value = null;
            foreach (var part in parts)
            	if (TryGetThemeVariable(part.Trim().Substring(1), out value))
                	break;
            if (string.IsNullOrEmpty(value))
            	value = m.Groups["Value"].Value;
            if (ApplicationServices.EnableMinifiedCss)
            	return ((before + value) 
                            + after);
            else
            	return ((before 
                            + (" /*" + variable)) 
                            + (("*/ " + value) 
                            + after));
        }
        
        protected bool TryGetThemeVariable(string name, out string value)
        {
            if (!(_themeVariables.TryGetValue(name, out value)))
            {
                JToken token = null;
                if (name.StartsWith("theme."))
                {
                    token = ApplicationServices.TryGetJsonProperty(_accent, string.Join(".", "theme", _theme["name"], name.Substring(6)));
                    if ((token == null) || (token.Type == JTokenType.Null))
                    	token = ApplicationServices.TryGetJsonProperty(_theme, name.Substring(6));
                }
                else
                {
                    token = ApplicationServices.TryGetJsonProperty(_accent, string.Join(".", "theme", _theme["name"], name));
                    if ((token == null) || (token.Type == JTokenType.Null))
                    	token = ApplicationServices.TryGetJsonProperty(_accent, name);
                }
                if ((token != null) && token.Type != JTokenType.Null)
                	value = ((string)(token));
                _themeVariables[name] = value;
            }
            return !(string.IsNullOrEmpty(value));
        }
    }
    
    public class FolderCacheDependency : CacheDependency
    {
        
        private FileSystemWatcher _watcher;
        
        public FolderCacheDependency(string dirName, string filter)
        {
            _watcher = new FileSystemWatcher(dirName, filter);
            _watcher.EnableRaisingEvents = true;
            _watcher.Changed += new FileSystemEventHandler(this.watcher_Changed);
            _watcher.Deleted += new FileSystemEventHandler(this.watcher_Changed);
            _watcher.Created += new FileSystemEventHandler(this.watcher_Changed);
            _watcher.Renamed += new RenamedEventHandler(this.watcher_Renamed);
        }
        
        void watcher_Renamed(object sender, RenamedEventArgs e)
        {
            NotifyDependencyChanged(this, e);
        }
        
        void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            NotifyDependencyChanged(this, e);
        }
    }
}
