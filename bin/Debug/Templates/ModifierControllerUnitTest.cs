using Moq;
using R6.API.xUnitTest.Helper;
using R6.Model.Common;
using System.Collections.Generic;
using Xunit;
using System.Threading.Tasks;
using R6.Model.Admin.ConstantSetup.ProcedureCode;

namespace R6.API.xUnitTest
{
    [Collection("FixtureCollection")]
    /// <summary>
    /// Test AccountNoteController
    /// </summary>
    public class ModifierControllerUnitTest
    {

        /// <summary>
        /// Injected MoqSerivceFixture hold all MoqServices.
        /// </summary>
        private readonly MoqSerivceFixture _moqSerivceFixture;

        /// Initializes a new instance of the <see cref="ModifierControllerUnitTest" /> class.
        /// </summary>
        public ModifierControllerUnitTest(MoqSerivceFixture moqSerivceFixture)
        {
            _moqSerivceFixture = moqSerivceFixture;
        }

        [Fact]
        public void GetAsyncTest()
        {
            //Arrange
            var response = new R6ResponseDto<ModifierDto>()
            {
                Data = new ModifierDto()
                {
                    Modifier_code = "COD",
                    Modifier_desc= "DESC",
                    Is_active = false,
                    Modifier_id= 12
                }
            };

            // Act
            var mockService = _moqSerivceFixture.ModifierService.Value;
            mockService.Setup(m => m.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(response).Verifiable();
            var controller = new ModifierController(mockService.Object);
            var actionResult = controller.GetByIdAsync(12);
            var result = Assert.IsType<Task<R6ResponseDto<ModifierDto>>>(actionResult);

            // Assert                    
            Assert.NotNull(result);
            var expectedData = ((R6.Model.Common.R6ResponseDto<ModifierDto>)result.Result);
            Assert.Equal(response.Data.Modifier_code, expectedData.Data.Modifier_code);
            Assert.Equal(response.Data.Modifier_id, expectedData.Data.Modifier_id);
        }


        [Fact]
        public void SearchAsyncTest()
        {
            //Arrange
            string modifier_code = "COD";
            string modifier_desc = "DESC";
            bool show_inactive = true;

            var response = new R6ResponseDto<List<ModifierDto>>()
            {
                Data = new List<ModifierDto>(){
                    new ModifierDto(){
                        Modifier_code = "COD",
                        Modifier_desc = "DESC",
                        Is_active = false,
                        Modifier_id = 12
                    }
                }
            };


            // Act
            var mockService = _moqSerivceFixture.ModifierService.Value;
            mockService.Setup(m => m.SearchAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(response).Verifiable();
            var controller = new ModifierController(mockService.Object);
            var actionResult = controller.SearchAsync(modifier_code, modifier_desc, show_inactive);
            var result = Assert.IsType<Task<R6ResponseDto<List<ModifierDto>>>>(actionResult);

            // Assert                    
            Assert.NotNull(result);
        }


        [Fact]
        public void AddAsyncTest()
        {
            //Arrange
            var request = new ModifierDto
            {
                Modifier_code = "COD",
                Modifier_desc = "DESC",
                Is_active = false
            };

            var response = new R6ResponseDto<int>()
            {
                Data = 12
            };

            // Act
            var mockService = _moqSerivceFixture.ModifierService.Value;
            mockService.Setup(m => m.AddAsync(It.IsAny<ModifierDto>())).ReturnsAsync(response).Verifiable();
            var controller = new ModifierController(mockService.Object);
            var actionResult = controller.AddAsync(request);
            var result = Assert.IsType<Task<R6ResponseDto<int>>>(actionResult);

            // Assert                    
            Assert.NotNull(result);
            var expectedData = ((R6.Model.Common.R6ResponseDto<int>)result.Result);
            Assert.Equal(response.Data, expectedData.Data);

        }



        [Fact]
        public void UpdateAsyncTest()
        {
            //Arrange
            var request = new ModifierDto
            {
                Modifier_code = "COD",
                Modifier_desc = "DESC",
                Is_active = false
            };

            var response = new R6ResponseDto<bool>()
            {
                Data = true
            };

            // Act
            var mockService = _moqSerivceFixture.ModifierService.Value;
            mockService.Setup(m => m.UpdateAsync(It.IsAny<ModifierDto>() )).ReturnsAsync(response).Verifiable();
            var controller = new ModifierController(mockService.Object);
            var actionResult = controller.UpdateAsync(12, request);
            var result = Assert.IsType<Task<R6ResponseDto<bool>>>(actionResult);

            // Assert                    
            Assert.NotNull(result);
            var expectedData = ((R6.Model.Common.R6ResponseDto<bool>)result.Result);
            Assert.Equal(response.Data, expectedData.Data);

        }


        [Fact]
        public void GetAllAsyncTest()
        {
            //Arrange
            var response = new R6ResponseDto<List<ModifierDto>>()
            {
                Data = new List<ModifierDto>(){
                    new ModifierDto(){
                        Modifier_code = "COD",
                        Modifier_desc = "DESC",
                        Is_active = false,
                        Modifier_id = 12
                    }
                }
            };


            // Act
            var mockService = _moqSerivceFixture.ModifierService.Value;
            mockService.Setup(m => m.GetAllAsync()).ReturnsAsync(response).Verifiable();
            var controller = new ModifierController(mockService.Object);
            var actionResult = controller.GetAllAsync();
            var result = Assert.IsType<Task<R6ResponseDto<List<ModifierDto>>>>(actionResult);

            // Assert                    
            Assert.NotNull(result);
        }


    }
}
