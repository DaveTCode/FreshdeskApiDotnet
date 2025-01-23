using System.Collections.Generic;
using System.Net.Http;

namespace FreshdeskApi.Client;

public interface IRequestWithAdditionalMultipartFormDataContent
{
    IEnumerable<(HttpContent HttpContent, string Name)> GetAdditionalMultipartFormDataContent();
}
