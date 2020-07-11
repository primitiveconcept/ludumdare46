namespace HackThePlanet
{
    public enum Port : ushort
    {
        Ftp = 21,
        Ssh = 22,
        Telnet = 23,
        Smtp = 25,
        Whois = 43,
        Dns = 53,
        Http = 80,
        Pop3 = 110,
        Imap4 = 143,
        Irc = 194,
        Ldap = 389,
        Https = 443,
        Smtps = 587,
        Imaps = 993,
        Pops = 995,
        Vpn = 1194,
        SapR3 = 3301, // Sometimes used in business operations
        Sql = 3306,
        Rdp = 3389,
        WinRM = 5985, // Windows Remote Management
        WinRMs = 5986, // Https version
        HttpAlt = 8080,
        VMWare = 8200,
        Traceroute = 33434
    }
}