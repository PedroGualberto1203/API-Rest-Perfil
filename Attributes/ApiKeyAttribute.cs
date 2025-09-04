using ApiPerfil;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Attributes;

[AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)] // Pode ser utilizado em classes ou métodos
public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync( // Implementação do método da interface IAsyncActionFilter
        ActionExecutingContext context, 
        ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Query.TryGetValue(Configuration.ApiKeyName, out var extractedApiKey)) // Essa linha tenta extrair o valor da chave de API da query string da requisição HTTP. Se a chave não for encontrada, a variável extractedApiKey será nula ou vazia.
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "ApiKey não encontrada"
            };
            return;
        }

        if (!Configuration.ApiKey.Equals(extractedApiKey)) // Aqui estamos comparando a chave extraída da query string com a chave de API armazenada na configuração do aplicativo. Se as chaves não coincidirem, a requisição é considerada não autorizada.
        {
            context.Result = new ContentResult()
            {
                StatusCode = 403,
                Content = "Acesso não autorixado"
            };
            return;
        }

        await next(); // Se a chave de API for válida, essa linha chama o próximo filtro ou ação no pipeline de processamento da requisição, permitindo que a requisição prossiga normalmente.

    }
}

//Um exeplo de acesso seria: localhost:5000?api_key=curso_api_IlTevUM/z0ey3NwCV/unWg==
//Aqui estaria certo, pois essa é realmente a chave que está no Configuration.cs
//Esta class basicamente verifica se a chave de API está presente na query string da requisição e se ela é válida. Se a chave estiver ausente ou incorreta, a requisição é bloqueada com um status de erro apropriado (401 para "não encontrado" e 403 para "acesso não autorizado"). Se a chave for válida, a requisição prossegue normalmente.