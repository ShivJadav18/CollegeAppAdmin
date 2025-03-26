

public class JwtMiddleware {
    
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next){
        _next = next;
    }

    public async Task Invoke(HttpContext context){
        
        var accessToken = context.Request.Cookies["jwtCookie"];

        if(!string.IsNullOrEmpty(accessToken)){

            context.Request.Headers.Append("Authorization", $"Bearer {accessToken}");
        }
        await _next(context);
    }

}