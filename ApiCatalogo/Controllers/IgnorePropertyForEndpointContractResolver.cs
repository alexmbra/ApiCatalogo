using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace ApiCatalogo.Controllers;

public class IgnorePropertyForEndpointContractResolver : DefaultContractResolver
{
    private readonly string _endpointName;
    private readonly IEnumerable<string> _propertyNamesToIgnore;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IgnorePropertyForEndpointContractResolver(string endpointName, IHttpContextAccessor httpContextAccessor, params string[] propertyNamesToIgnore)
    {
        _endpointName = endpointName;
        _propertyNamesToIgnore = propertyNamesToIgnore;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);

        if (_propertyNamesToIgnore.Contains(property.PropertyName) && _httpContextAccessor.HttpContext.Request.Path.Value.Contains(_endpointName))
        {
            property.ShouldSerialize = instance => false;
        }

        return property;
    }
}




