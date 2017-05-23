using System;
using System.Linq;
using System.Transactions;
using EGM.GameMachines.Core.DataAccess;
using EGM.GameMachines.Core.Repository;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Shouldly;
using Xunit;

namespace EGM.GameMachines.Test.Repository
{
    /// <summary>
    /// Unit test class to check GameMachineRepository functionalities
    /// </summary>
    public class GameMachineRepositoryTest
    {
        IGameMachineRepository sut;
        IFixture _fixture;

        public GameMachineRepositoryTest()
        {
            sut = new GameMachineRepository();
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Customize<GameMachineRepositoryTest>(t => t.OmitAutoProperties());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(5));
        }
       
        [Fact]
        public void GameMachineRepository_AddGamemachine_with_GameData()
        { //given //when
            using (var transaction = new TransactionScope())
            {//then
                var result = sut.AddGamemachine(CreateMockGameMachine());
                result.ShouldBeTrue();                
            }

        }
        [Fact]
        public void GameMachineRepository_AddGamemachine_without_GameData()
        { 
            using (var transaction = new TransactionScope())
            {
                var result = sut.AddGamemachine(null);
                result.ShouldBeFalse();
            }

        }
        [Fact]
        public void GameMachineRepository_UpdateGamemachine_with_GameData()
        { 
            using (var transaction = new TransactionScope())
            {
                GameMachine gm = CreateMockGameMachine();
                GameMachine ugm = CreateMockGameMachine();
                sut.AddGamemachine(gm);
                ugm.Id = gm.Id;
                var result = sut.UpdateGamemachine(ugm);
                result.ShouldBeTrue();
            }

        }
        [Fact]
        public void GameMachineRepository_UpdateGamemachine_without_GameData()
        {
            using (var transaction = new TransactionScope())
            {                
                var result = sut.UpdateGamemachine(null);
                result.ShouldBeFalse();
            }

        }
        [Fact]
        public void GameMachineRepository_DeleteGamemachine_with_GameId()
        {
            using (var transaction = new TransactionScope())
            {
                GameMachine gm = CreateMockGameMachine();                
                sut.AddGamemachine(gm);                
                var result = sut.DeleteGamemachine(Convert.ToString(gm.Id));
                result.ShouldBeTrue();
            }

        }
        [Fact]
        public void GameMachineRepository_DeleteGamemachine_with_GameIdEmpty()
        {
            using (var transaction = new TransactionScope())
            {
                var result = sut.DeleteGamemachine(string.Empty);
                result.ShouldBeFalse();
            }

        }

        [Fact]
        public void GameMachineRepository_DeleteGamemachine_with_GameIdNull()
        {
            using (var transaction = new TransactionScope())
            {
                var result = sut.DeleteGamemachine(null);
                result.ShouldBeFalse();
            }

        }
        [Fact]
        public void GameMachineRepository_GetAllGamemachines_with_GameData()
        {
            using (var transaction = new TransactionScope())
            {
                GameMachine gm = CreateMockGameMachine();                
                sut.AddGamemachine(gm);                
                var result = sut.GetAllGameMachines();
                result.Count().ShouldBeGreaterThan(0);
            }

        }        
        [Fact]
        public void GameMachineRepository_GetGamemachineById_with_GameData()
        {
            using (var transaction = new TransactionScope())
            {
                GameMachine gm = CreateMockGameMachine();
                sut.AddGamemachine(gm);
                var result = sut.GetGamemachineById(Convert.ToString(gm.Id));
                result.ShouldNotBeNull();
            }

        }
        [Fact]
        public void GameMachineRepository_GetGamemachineById_without_GameData()
        {
            using (var transaction = new TransactionScope())
            {
                var result = sut.GetGamemachineById("");
                result.ShouldBeNull();
            }

        }

        //MockData
        private GameMachine CreateMockGameMachine()
        {
            return _fixture.Create<GameMachine>();
        }        
    }
}
