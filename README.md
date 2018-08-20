# Reproduction

Sample test solution you can use to easily reproduce issues or demonstrate features

The following shows how you would setup a simple test by specifying the script, server and assertions, All the heavy lifting and setup is done for you
You could also adjust TestHelper.MaxTestWaitTime to suit your test scenario. Enjoy!

        [TestMethod]
        public void it_should_call_server_from_client()
        {
            TestHelper.MaxTestWaitTime = TimeSpan.FromSeconds(20);
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
                            SignalX.RespondToAll(request.ReplyTo, request.Message);
                        });
                },
                checks: () =>
                {
                    Assert.IsTrue(true);
                }
            );
            TestHelper.RunScenario(scenario);
        }
