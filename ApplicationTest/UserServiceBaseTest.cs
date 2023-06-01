using Application.Enums;
using Application.Exceptions;
using Application.Services.UserServices;

namespace ApplicationTest;

[TestClass]
public class UserServiceBaseTest : Utils
{
    [TestMethod]
    public void ExitTest()
    {
        // A
        IUserService user = ValidUserService();
        user.Exit();
        
        // A, A
        Assert.ThrowsException<InvalidSessionException>(() => user.EditLogin(""));
    }

    [TestMethod]
    public void EditLoginTest_Happy()
    {
        var user = ValidClient();
        IUserService userService = ValidServiceFromClient(user);

        var result = userService.EditLogin("tester");
        
        Assert.AreEqual(result, EditLoginResult.Success);
        Assert.AreEqual(user.Login, "tester");
    }

    [TestMethod]
    public void EditLoginTest_BadLogin()
    {
        var user = ValidClient();
        IUserService userService = ValidServiceFromClient(user);

        var result = userService.EditLogin("123");
        
        Assert.AreEqual(EditLoginResult.DoesNotMeetReqs, result);
        Assert.AreNotEqual("123", user.Login);
    }
    
    [TestMethod]
    public void SetPassword_ValidPassword_PasswordSetSuccessfully()
    {
        // Arrange
        var user = ValidClient();
        IUserService userService = ValidServiceFromClient(user);

        // Act
        var result = userService.EditPassword("Password!1");
        // Assert
        Assert.AreEqual(EditPasswordResult.Success, result);
        Assert.AreEqual("Password!1", user.Password);
    }

    [TestMethod]
    public void EditPasswordBadPassword()
    {
        // Arrange
        var user = ValidClient();
        IUserService userService = ValidServiceFromClient(user);

        // Act
        var result = userService.EditPassword("password");
        // Assert
        Assert.AreEqual(EditPasswordResult.DoesNotMeetReqs, result);
        Assert.AreNotEqual("password", user.Password);
    }
}