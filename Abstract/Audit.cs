namespace Abstract;
// Audit sınıfı bir nevi loglama işlevi görerek kimin ne zaman ne yaptığını izlemek içindir.
public abstract class Audit {
    public int CUser_id {get;set;}
    public DateTime CDate {get;set;}
    public int MUser_id {get;set;}
    public DateTime MDate {get;set;}
}