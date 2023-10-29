using InstTracker.Data;
using InstTracker.Data.Model;
using InstTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InstTracker.Services
{
    internal class CharacterService : ICharacterService
    {
        private readonly AppDbContext _dbContext;

        public CharacterService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Character>> GetAllCharactersAsync() => 
            await _dbContext.Characters.OrderBy(c => c.Id).ToListAsync();

        public async Task<string> AddCharacter(string name)
        {
            try
            {
                var character = new Character { Name = name };
                var id = await _dbContext.Characters.AddAsync(character);
                await _dbContext.SaveChangesAsync();

                return id.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public async Task<string> RemoveCharacter(Expression<Func<Character, bool>> predicate)
        {
            try
            {
                var character = await _dbContext.Characters.FirstOrDefaultAsync(predicate);

                if (character is null)
                {
                    return "Персонаж не найден";
                }

                _dbContext.Characters.Remove(character);

                await _dbContext.SaveChangesAsync();

                return "Удалено";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return null;
        }



    }
}
