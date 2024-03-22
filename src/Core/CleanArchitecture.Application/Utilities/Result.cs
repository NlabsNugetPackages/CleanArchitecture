using System.Net;

namespace CleanArchitecture.Application.Utilities;
public sealed class Result<T>
{
    public T? Data { get; set; }
    public List<string>? ErrorMessages { get; set; }
    public bool IsSuccessful { get; set; } = true;
    public int StatusCode { get; set; } = (int)HttpStatusCode.OK;

    public Result(T data)
    {
        Data = data;
    }

    public Result(int statusCode, List<string> errorMessages)
    {
        IsSuccessful = false;
        StatusCode = statusCode;
        ErrorMessages = errorMessages;
    }

    public Result(int statusCode, string errorMessage)
    {
        IsSuccessful = false;
        StatusCode = statusCode;
        ErrorMessages = new() { errorMessage };
    }

    public static implicit operator Result<T>(T data)
    {
        return new(data);
    }

    public static implicit operator Result<T>((int statusCode, List<string> errorMessages) parameters)
    {
        return new(parameters.statusCode, parameters.errorMessages);
    }

    public static implicit operator Result<T>((int statusCode, string errorMessage) parameters)
    {
        return new(parameters.statusCode, parameters.errorMessage);
    }


    public static Result<T> Succeed(T data)
    {
        return new(data);
    }

    public static Result<T> Failure(int statusCode, List<string> errorMessages)
    {
        return new(statusCode, errorMessages);
    }

    public static Result<T> Failure(int statusCode, string errorMessage)
    {
        return new(statusCode, errorMessage);
    }

    public static Result<T> Failure(string errorMessage)
    {
        return new(500, errorMessage);
    }

    public static Result<T> Failure(List<string> errorMessages)
    {
        return new(500, errorMessages);
    }
}

/*
Kullanım örnekleri
var successResult = new Result<string>("İşlem başarılı.");
Alternatif olarak, veriden örtük dönüşümü kullanabilirsiniz:
Result<string> result = "İşlem başarılı.";
Başarısızlık durumları için, bir HTTP durum kodu ve hata mesajları içeren bir Result nesnesi oluşturun:
var errorResult = new Result<string>(400, new List<string> { "Hata 1", "Hata 2" });
Ya da hata detaylarından örtük dönüşümü kullanabilirsiniz:
Result<string> result = (400, new List<string> { "Hata 1", "Hata 2" });
Tek bir hata mesajı için:
Result<string> result = (400, "Tek hata mesajı");
Başarı için Succeed yöntemini kullanarak:
Result<string> result = Result<string>.Succeed("Başarılıdır");
Hata için Failure yöntemini kullanarak:
One error message
Result<string> result = Result<string>.Failure(500,"Başarısızdır!");
Birden fazla hata mesajı
Result<string> result = Result<string>.Failure(500,new List<string>() {"Başarısızdır!","Benzer değil!"});
Tek bir hata mesajıyla 500 durum kodunu döndürün
Result<string> result = Result<string>.Failure("Başarısızdır!"); //500 status kodunu döndürür
Birden fazla hata mesajıyla 500 durum kodunu döndürün
Result<string> result = Result<string>.Failure(new List<string>() {"Başarısızdır!","Benzer değil!"});
*/