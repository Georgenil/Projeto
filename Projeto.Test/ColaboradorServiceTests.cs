using Projeto.Domain.Models;
using Projeto.Service;

namespace Projeto.Test
{
    public class ColaboradorServiceTests
    {
        private readonly ColaboradorService _colaboradorService;
        private readonly Mock<IColaboradorRepository> _colaboradorRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;


        public ColaboradorServiceTests()
        {
            _colaboradorRepositoryMock = new Mock<IColaboradorRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _colaboradorService = new ColaboradorService(_unitOfWorkMock.Object, _colaboradorRepositoryMock.Object);
        }

        [Fact(DisplayName = "Buscar uma lista de colaboradores com sucesso.")]
        public async Task BuscarTodos_Sucesso()
        {
            // Arrange
            var colaboradores = new List<Colaborador>()
            {
                new() { Id = 1, Nome = "Colaborador 1"},
                new() { Id = 2, Nome = "Colaborador 2" }
            };

            _colaboradorRepositoryMock.Setup(rep => rep.GetAllAsync()).ReturnsAsync(colaboradores);

            // Act
            var result = await _colaboradorService.BuscarTodos();

            // Assert
            Assert.Equal(colaboradores, result.Entity);
        }
    }
}