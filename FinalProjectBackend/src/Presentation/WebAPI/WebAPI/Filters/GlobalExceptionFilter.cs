using Application.Common.Models.Errors;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace WebAPI.Filters
{
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {


        public  Task OnExceptionAsync(ExceptionContext context)
        {
            ApiErrorDto apiErrordto = new ApiErrorDto();

            switch(context.Exception) {


                case ValidationException:

                    var validationException = context.Exception as ValidationException;
                    

                    var propertyNames =validationException.Errors.Select(x=>x.PropertyName)
                        .Distinct()  ;
                    //birden fazla geldiyse
                    foreach(var propertyName in propertyNames)
                    {


                        var propertyFailures = validationException.Errors
                            .Where(e => e.PropertyName == propertyName)
                            .Select(x => x.ErrorMessage)
                            .ToList();
                        apiErrordto.Errors.Add(new ErrorDto(propertyName, propertyFailures));
                    }

                    apiErrordto.Message = "One or more validation errors were occured ";
                //    context.HttpContext.Response.StatusCode = (int)StatusCodes.Status400BadRequest;
                    context.Result=new BadRequestObjectResult(apiErrordto);
                  
                    break;
                 



                default:
                    apiErrordto.Message = "An unexpected error was occured... ";
                    //  context.HttpContext.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
                    context.Result = new BadRequestObjectResult(apiErrordto)
                    {

                        StatusCode = (int)StatusCodes.Status500InternalServerError
                    };



                    break;
            
            }
            return Task.CompletedTask;
            //context.HttpContext.Response.ContentType = "application/json";

            //var apiErrorDtoJson = JsonSerializer.Serialize(apiErrordto);

            //await context.HttpContext.Response.WriteAsync(apiErrorDtoJson);
            // return Task.CompletedTask;
        }
    }
}
