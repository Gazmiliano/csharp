// *
// Данный код лучше поместить в отделый файл например [Utils]
// *
// Что такое Utils? 
// Это класс, там могут быть расположены всякие коды
// Чтобы использовать данный код, достаточно скопировать весь код и подставить
// *


// Поместить в глобальную переменную можно в самом классе
public static string KEYString = "cOaJTJQM8dSQ66d5";
public static string IVString = "0kdkpHVAUoJRmSK8";

// Шифрование логина и пароля
public static string Encryptor(string text)
{
    if (text == "") return text;
    byte[] plaintextbytes = ASCIIEncoding.ASCII.GetBytes(text);
    AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
    aes.BlockSize = 128;
    aes.KeySize = 256;
    aes.Key = ASCIIEncoding.ASCII.GetBytes(KEYString);
    aes.IV = ASCIIEncoding.ASCII.GetBytes(IVString);
    aes.Padding = PaddingMode.PKCS7;
    aes.Mode = CipherMode.CBC;
    ICryptoTransform crypto = aes.CreateEncryptor(aes.Key, aes.IV);
    byte[] encrypted = crypto.TransformFinalBlock(plaintextbytes, 0, plaintextbytes.Length);
    crypto.Dispose();
    string enc = BitConverter.ToString(encrypted).Replace("-", string.Empty);
    return (enc);
}

// Расшивровка логина и пароля
public static string Decryptor(string enc)
{
    if (enc == "") return enc;
    string hexstr = enc;
    byte[] data = ConvertFromStringToHex(hexstr);
    string base64 = Convert.ToBase64String(data);
    byte[] encryptedbytes = Convert.FromBase64String(base64);
    AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
    aes.BlockSize = 128;
    aes.KeySize = 256;
    aes.Key = ASCIIEncoding.ASCII.GetBytes(KEYString);
    aes.IV = ASCIIEncoding.ASCII.GetBytes(IVString);
    aes.Padding = PaddingMode.PKCS7;
    aes.Mode = CipherMode.CBC;
    ICryptoTransform crypto = aes.CreateDecryptor(aes.Key, aes.IV);
    byte[] secret = crypto.TransformFinalBlock(encryptedbytes, 0, encryptedbytes.Length);
    crypto.Dispose();
    return ASCIIEncoding.ASCII.GetString(secret);
}

// ?
public static byte[] ConvertFromStringToHex(string hexstr)
{
    byte[] resultantArray = new byte[hexstr.Length / 2];
    for (int i = 0; i < resultantArray.Length; i++)
    {
        resultantArray[i] = Convert.ToByte(hexstr.Substring(i * 2, 2), 16);
    }
    return resultantArray;
}