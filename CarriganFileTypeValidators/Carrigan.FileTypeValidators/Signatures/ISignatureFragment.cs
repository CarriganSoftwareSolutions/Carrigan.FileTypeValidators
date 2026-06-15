

namespace Carrigan.FileTypeValidators.Signatures;

/// <summary>
/// Defines a contract for a signature fragment that can be used to validate file types based on specific patterns in the data.
/// </summary>
public interface ISignatureFragment
{
    /// <summary>
    /// Determines whether the specified data matches the signature fragment's criteria.
    /// </summary>
    /// <param name="data">
    /// The data to be checked against the signature fragment. This is typically a sequence of bytes read from a file.
    /// The implementation of this method will define how the data is evaluated to determine if it matches the signature fragment's criteria.
    /// </param>
    /// <returns
    /// >true if the data matches the signature fragment's criteria; otherwise, false. 
    /// The exact meaning of a "match" is determined by the implementation of the signature fragment.
    /// ></returns>
    bool IsMatch(IEnumerable<byte> data);
}
