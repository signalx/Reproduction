# Reproduction

Sample test solution you can use to easily reproduce issues or demonstrate features

The following shows how you would setup a simple test by specifying the script, server and assertions, All the heavy lifting and setup is done for you
You could also adjust TestHelper.MaxTestWaitTime to suit your test scenario. Enjoy!


```
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
```
