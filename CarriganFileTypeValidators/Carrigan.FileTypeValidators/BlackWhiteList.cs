using Carrigan.Core.Extensions;

namespace Carrigan.FileTypeValidators;

public class BlackWhiteList<T> where T : IEquatable<T>
{
    private Dictionary<T, bool> _whiteBlackList = [];

    public BlackWhiteList(params T[] values) 
        => AddWhiteListValues(values);

    public void AddWhiteListValues(params T[] values) =>
        values.ForEach(item =>
        {
            if (_whiteBlackList.TryGetValue(item, out bool value) && !value)
                throw new InvalidOperationException($"The item {item} cannot be added to the whitelist because it already exists in the blacklist.");
            else
                _whiteBlackList[item] = true;
        });

    public void AddBlackListValues(params T[] values) => 
        values.ForEach( item=>
        {
            if(_whiteBlackList.TryGetValue(item, out bool value) && value)
                throw new InvalidOperationException($"The item { item } cannot be added to the blacklist because it already exists in the whitelist.");
            else
                _whiteBlackList[item] = false;
        });

    public bool IsAllowed(T value) =>
        _whiteBlackList.ContainsKey(value) && _whiteBlackList[value];
}
