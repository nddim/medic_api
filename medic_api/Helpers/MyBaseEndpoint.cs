using Microsoft.AspNetCore.Mvc;

namespace medic_api.Helpers
{
    [ApiController]
    public abstract class MyBaseEndpoint<TRequest, TResponse> : ControllerBase
    {
        public abstract Task <ActionResult<TResponse>> Obradi(TRequest request, CancellationToken cancellation = default);
    }
}
