using Carrigan.FileTypeValidators.Abstracts;
using Carrigan.FileTypeValidators.Enums;
using System.Reflection;

namespace Carrigan.FileTypeValidators;

internal static class MimeTypeDefinitionFactory
{
    private static readonly Dictionary<string, Type> _mimeTypeByMimeString = [];
    private static readonly Dictionary<MimeType, Type> _mimeTypeMimeTypeEnum = [];

    static MimeTypeDefinitionFactory()
    {
        // Get the abstract type
        Type abstractType = typeof(MimeTypeDefinition);

        // Get all types in the current assembly
        IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes();

        // Filter types that implement the interface
        IEnumerable<Type> implementingTypes = types.Where(t => abstractType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

        // Cycle through the implementing types and create instances
        foreach (Type type in implementingTypes)
        {
            // Create an instance of the class
            MimeTypeDefinition instance = (MimeTypeDefinition)Activator.CreateInstance(type);
            if (instance != null)
            {
                // register the instance
                _mimeTypeByMimeString[instance.MimeType] = instance.GetType();
                _mimeTypeMimeTypeEnum[instance.MimeTypeEnum] = instance.GetType();
            }
        }
    }

    internal static MimeTypeDefinition GetDefinition(string mimeType)
    {
        MimeTypeDefinition handler = null;
        if (_mimeTypeByMimeString.TryGetValue(mimeType.ToLower(), out Type? handlerType))
        {
            handler = (MimeTypeDefinition)Activator.CreateInstance(handlerType);
        }
        if (handler == null)
            throw new ArgumentException($"No definition found for MIME type: {mimeType}");
        else
            return handler;
    }

    internal static MimeTypeDefinition GetDefinition(MimeType mimeType)
    {
        MimeTypeDefinition handler = null;
        if (_mimeTypeMimeTypeEnum.TryGetValue(mimeType, out Type? handlerType))
        {
            handler = (MimeTypeDefinition)Activator.CreateInstance(handlerType);
        }
        if (handler == null)
            throw new ArgumentException($"No definition found for MIME type: {mimeType}");
        else
            return handler;
    }
}
