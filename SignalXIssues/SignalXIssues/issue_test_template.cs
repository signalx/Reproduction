using Microsoft.VisualStudio.TestTools.UnitTesting;
using SignalXLib.Lib;
using SignalXLib.TestHelperLib;

namespace SignalXIssues
{
    [TestClass]
    public class issue_test_template
    {
        [TestMethod]
        public void it_should_call_server_from_client()
        {
            SignalXTester.Run(
                (signalx, assert) =>
                {
                    string receivedMessage = "";
                    return new SignalXTestDefinition(
                        @"signalx.ready(function (server) {
							      server.myServer('abc',function (message) {
							       });
                                 });",
                        () =>
                        {
                            signalx.Server("myServer",
                                request =>
                                {
                                    receivedMessage = request.Message as string;
                                    signalx.RespondToAll(request.ReplyTo, receivedMessage);
                                });
                        },
                        () =>
                        {
                            Assert.AreEqual("abc", receivedMessage);
                        }
                    );
                });
        }
    }
}