using Application.Interfaces;

namespace UnitTest.Fakes;

public class FakePasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return $"{password}_hashed";
    }

    public bool Verify(string password, string hash)
    {
        return $"{password}_hashed" == hash;
    }
}
