using InstTracker.Data.Model;
using System.Linq.Expressions;

namespace InstTracker.Services.Interfaces
{
    public interface ICharacterService
    {
        Task<string> AddCharacter(string name);

        Task<Character> Find(Expression<Func<Character, bool>> predicate);

        Task<List<Character>> GetAllCharactersAsync();

        Task<string> RemoveCharacter(Expression<Func<Character, bool>> predicate);
    }
}