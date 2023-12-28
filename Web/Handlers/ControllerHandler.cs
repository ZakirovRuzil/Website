using System.Collections.Specialized;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Web.Attributes;
using Web.ORM.Entities;
using Formatting = System.Xml.Formatting;

namespace Web.Handlers;

public class ControllersHandler: Handler
{
    public override void HandleRequest(HttpListenerContext context)
    {
        var uriSegments = context.Request.Url!.Segments;
        var request = context.Request;
        var response = context.Response;
        object? res = null;
        
        string[] strParams = uriSegments
            .Skip(1)
            .Select(s => s.Replace("/", ""))
            .ToArray();
        
        if (strParams.Length < 2)
            return;
        
        var controllerName = strParams[^2];
        var methodName = strParams[^1];

        var id = request.QueryString["id"];
        

        var assembly = Assembly.GetExecutingAssembly();
        var tt = assembly.GetTypes();
        var controller = assembly
            .GetTypes()
            .Where(t => Attribute.IsDefined(t, typeof(HttpControllerAttribute)))
            .FirstOrDefault(c =>
                ((HttpControllerAttribute)Attribute.GetCustomAttribute(c, typeof(HttpControllerAttribute))!).Name.Equals(controllerName));
        
        var method = (controller?.GetMethods())
            .FirstOrDefault(x => x.GetCustomAttributes(true)
                .Any(attr => attr.GetType().Name.Equals($"{request.HttpMethod}Attribute",
                                 StringComparison.OrdinalIgnoreCase) &&
                             ((HttpMethodAttribute)attr).ActionName.Equals(methodName,
                                 StringComparison.OrdinalIgnoreCase)));

        object[] queryParams = Array.Empty<object>();
        
        
        if (request is { HttpMethod: "GET" }
            && methodName.Equals("DeleteBoss", StringComparison.OrdinalIgnoreCase))
        {
            res =  method?.Invoke(Activator.CreateInstance(controller!), new object[] { id });
            ProcessResult(res, response);
        }
        else if (request is { HttpMethod: "GET" }
                 && methodName.Equals("AddBoss", StringComparison.OrdinalIgnoreCase))
        {
            queryParams = GetFromGetMethod(context);
            res =  method?.Invoke(Activator.CreateInstance(controller!), queryParams );
            ProcessResult(res, response);
        }
        
        else if (request is { HttpMethod: "GET" }
                 && methodName.Equals("GetBosses", StringComparison.OrdinalIgnoreCase))
        {
            res =  method?.Invoke(Activator.CreateInstance(controller!), queryParams );
            ProcessResult(res, response);
        }
        
        else if (request is { HttpMethod: "GET" }
                 && methodName.Equals("EditBoss", StringComparison.OrdinalIgnoreCase))
        {
            queryParams = GetFromGetMethodForEdit(context);
            res =  method?.Invoke(Activator.CreateInstance(controller!), queryParams );
            ProcessResult(res, response);
        }
        context.Response.ContentType = "text/html";
    }
    private object[] GetFromGetMethod(HttpListenerContext context)
    {
        // string postData;
        
        var name = context.Request.QueryString.GetValues("Name")[0];
        var picture = context.Request.QueryString.GetValues("Picture")[0];
        var discription = context.Request.QueryString.GetValues("Discription")[0];
        
        Boss boss = new Boss
        {
            Name = name,
            Picture = picture,
            Discription = discription
        };
        
        return new object[]{boss};
    }
    
    private object[] GetFromGetMethodForEdit(HttpListenerContext context)
    {
        string postData;

        var id = context.Request.QueryString.GetValues("Id")[0];
        var name = context.Request.QueryString.GetValues("Name")[0];
        var picture = context.Request.QueryString.GetValues("Picture")[0];
        var discription = context.Request.QueryString.GetValues("Discription")[0];
        
        Boss boss = new Boss
        {
            Id = int.Parse(id),
            Name = name,
            Picture = picture,
            Discription = discription
        };
        
        return new object[]{boss};
    }

    private static void ProcessResult<T>(T result, HttpListenerResponse response)
    {
        switch (result)
        {
            case string resultOfString:
            {
                response.ContentType = ExtensionsFile._getExtension[".html"];
                var buffer = Encoding.UTF8.GetBytes(resultOfString);
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
                break;
            }
            case T[] arrayOfObjects:
            {
                response.ContentType = ExtensionsFile._getExtension["json"];
                var json = JsonConvert.SerializeObject(arrayOfObjects);
                var buffer = Encoding.UTF8.GetBytes(json);
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
                break;
            }
        }
    }
}

internal class ExtensionsFile
{
    public static readonly Dictionary<string, string> _getExtension = new()
    {
        [".css"] = "text/css",
        [".html"] = "text/html",
        [".jpg"] = "image/jpeg",
        [".jpeg"] = "image/jpeg",
        [".svg"] = "image/svg+xml",
        [".png"] = "image/png",
        [".json"] = "application/json",
        [".webp"] = "image/webp",
        [".ico"] = "image/x-icon"
    };
}