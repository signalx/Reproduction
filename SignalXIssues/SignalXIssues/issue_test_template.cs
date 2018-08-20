using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SignalXIssues
{
    using SignalXLib.Lib;
    using SignalXLib.TestHelperLib;

    [TestClass]
    public class issue_test_template
    {
        [TestMethod]
        public void it_should_call_server_from_client()
        {
         
            TestHelper.MaxTestWaitTime = TimeSpan.FromSeconds(20);
            string receivedMessage = "";
            var scenario = new ScenarioDefinition(
                script: @"signalx.ready(function (server) {
							      server.myServer('abc',function (message) {
							       });
                                 });",
                server: () =>
                {
                    SignalX.Server("myServer",
                        request =>
                        {
                            receivedMessage = request.Message.ToString();
                            SignalX.RespondToAll(request.ReplyTo, request.Message);
                        });
                },
                checks: () =>
                {
                    Assert.AreEqual("abc", receivedMessage);
                }
            );
            TestHelper.RunScenario(scenario);
        }
    }
}