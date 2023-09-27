using System.Text.RegularExpressions;

namespace Integrated.Validation;

public class ValidationAttribute
{
    public (bool isSuccessful, string Message) IsValidPhoneNumber(string phone_number)
    {
        if (phone_number is null) return (false, "Phone Number can not be null!");

        Regex regex = new Regex(@"^[\+][0-9]{3}?[0-9]{9}$");
        if (regex.Match(phone_number.ToString()!).Success)
            return (true, " ");

        return (false, "Enter correct phone number!");
    }
    public (bool isSuccessful, string Message) IsValidName(string name)
    {
        if (name is null) return (false, "Name can not be null!");

        Regex regex = new Regex(@"^[a-z_',A-Z_']{3,25}$");
        if (regex.Match(name.ToString()!).Success)
            return (true, " ");

        return (false, "Enter correct name!");
    }
    public (bool isSuccessful, string Message) IsValidSurname(string name)
    {
        if (name is null) return (false, "Surname can not be null!");

        Regex regex = new Regex(@"^[a-z_',A-Z_']{3,25}$");
        if (regex.Match(name.ToString()!).Success)
            return (true, " ");

        return (false, "Enter correct surname!");
    }
    public (bool isSuccessful, string Message) IsValidAdress(string adress)
    {
        if (adress is null) return (false, "Addres can not be null!");

        Regex regex = new Regex(@"^[A-Za-z0-9 -]{3,60}$");
        if (regex.Match(adress.ToString()!).Success)
            return (true, " ");

        return (false, "Enter correct addres!");
    }
    public (bool isSuccessful, string Message) IsValidDate(string date)
    {
        if (date is null) return (false, "Date can not be null!");

        Regex regex = new Regex(@"(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})");
        if (regex.Match(date.ToString()!).Success)
            return (true, " ");

        return (false, "Enter correct date!");
    }
    public (bool isSuccessful, string Message) IsValidRegion(string name)
    {
        if (name is null) return (false, "Region can not be null!");

        Regex regex = new Regex(@"^[A-Za-z0-9 -]{3,30}$");
        if (regex.Match(name.ToString()!).Success)
            return (true, " ");

        return (false, "Enter correct region!");
    }
    public (bool isSuccessful, string Message) IsValidDistrict(string name)
    {
        if (name is null) return (false, "District can not be null!");

        Regex regex = new Regex(@"^[A-Za-z0-9 -]{3,30}$");
        if (regex.Match(name.ToString()!).Success)
            return (true, " ");

        return (false, "Enter correct destrict!");
    }
    public (bool isSuccessful, string Message) IsValidPassport(string name)
    {
        if (name is null) return (false, "Passport Seria Number can not be null!");

        Regex regex = new Regex(@"^[A-Z]{2}\d{7}$");
        if (regex.Match(name.ToString()!).Success)
            return (true, " ");

        return (false, "Enter correct Passport Seria Number!");
    }
}
