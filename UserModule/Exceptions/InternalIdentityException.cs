using System.Text;
using Microsoft.AspNetCore.Identity;

namespace UserModule.Exceptions;

public class InternalIdentityException(IEnumerable<IdentityError> errors) : Exception(string.Join(" ", errors.Select(e => e.Description))) { }