using System;
using System.Linq;

namespace SpreetailWorkSampleDavidOBrien
{
    public class AppTest
    {
        public void Run()
        {
            TestAppAdd();
            TestAppAllMembers();
            TestAppClear();
            TestAppItems();
            TestAppKeyExists();
            TestAppKeyExistsBool();
            TestAppKeys();
            TestAppMemberExistsBool();
            TestAppMembers();
            TestAppRemove();
            TestAppRemoveAll();
        }

        private void TestAppAdd()
        {
            var isPassed = true;

            var mockApp = new App(0);
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "bar")[0] == "Added";
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "baz")[0] == "Added";
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "bar")[0] == "ERROR, member already exists for key";

            Console.WriteLine($"TEST: TestAppAdd Passed: {isPassed}");
        }

        private void TestAppAllMembers()
        {
            var isPassed = true;
            
            var mockApp = new App(0);
            isPassed = !isPassed ? isPassed : mockApp.AllMembers().Count() == 0;
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "bar")[0] == "Added";
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "baz")[0] == "Added";
            var result = mockApp.AllMembers();

            isPassed = !isPassed ? isPassed : result[0] == "bar";
            isPassed = !isPassed ? isPassed : result[1] == "baz";
            isPassed = !isPassed ? isPassed : mockApp.Add("bang", "bar")[0] == "Added";
            isPassed = !isPassed ? isPassed : mockApp.Add("bang", "baz")[0] == "Added";
            result = mockApp.AllMembers();
            isPassed = !isPassed ? isPassed : result[0] == "bar";
            isPassed = !isPassed ? isPassed : result[1] == "baz";
            isPassed = !isPassed ? isPassed : result[2] == "bar";
            isPassed = !isPassed ? isPassed : result[3] == "baz";
            
            Console.WriteLine($"TEST: TestAppAllMembers Passed: {isPassed}");
        }

        private void TestAppClear()
        {
            var isPassed = true;
            
            var mockApp = new App(0);
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "bar")[0] == "Added";
            isPassed = !isPassed ? isPassed : mockApp.Add("bang", "zip")[0] == "Added";
            var result = mockApp.Keys();

            isPassed = !isPassed ? isPassed : result[0] == "foo";
            isPassed = !isPassed ? isPassed : result[1] == "bang";
            isPassed = !isPassed ? isPassed : mockApp.Clear()[0] == "Cleared";
            isPassed = !isPassed ? isPassed : mockApp.Keys().Count() == 0;
            isPassed = !isPassed ? isPassed : mockApp.Clear()[0] == "Cleared";
            isPassed = !isPassed ? isPassed : mockApp.Keys().Count() == 0;
            
            Console.WriteLine($"TEST: TestAppClear Passed: {isPassed}");
        }

        private void TestAppItems()
        {
            var isPassed = true;
            
            var mockApp = new App(0);
            isPassed = !isPassed ? isPassed : mockApp.Items().Count() == 0;
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "bar")[0] == "Added";
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "baz")[0] == "Added";
            var result = mockApp.Items();

            isPassed = !isPassed ? isPassed : result[0] == "foo: bar";
            isPassed = !isPassed ? isPassed : result[1] == "foo: baz";
            isPassed = !isPassed ? isPassed : mockApp.Add("bang", "bar")[0] == "Added";
            isPassed = !isPassed ? isPassed : mockApp.Add("bang", "baz")[0] == "Added";
            result = mockApp.Items();
            isPassed = !isPassed ? isPassed : result[0] == "foo: bar";
            isPassed = !isPassed ? isPassed : result[1] == "foo: baz";
            isPassed = !isPassed ? isPassed : result[2] == "bang: bar";
            isPassed = !isPassed ? isPassed : result[3] == "bang: baz";
            
            Console.WriteLine($"TEST: TestAppItems Passed: {isPassed}");
        }

        private void TestAppKeyExists()
        {
            var isPassed = true;
            
            var mockApp = new App(0);
            isPassed = !isPassed ? isPassed : mockApp.KeyExists("foo")[0] == "false";
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "bar")[0] == "Added";
            isPassed = !isPassed ? isPassed : mockApp.KeyExists("foo")[0] == "true";
            
            Console.WriteLine($"TEST: TestAppKeyExists Passed: {isPassed}");
        }

        private void TestAppKeyExistsBool()
        {
            var isPassed = true;
            
            var mockApp = new App(0);
            isPassed = !isPassed ? isPassed : mockApp.KeyExistsBool("foo") == false;
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "bar")[0] == "Added";
            isPassed = !isPassed ? isPassed : mockApp.KeyExistsBool("foo") == true;
            
            Console.WriteLine($"TEST: TestAppKeyExistsBool Passed: {isPassed}");
        }

        private void TestAppKeys()
        {
            var isPassed = true;
            
            var mockApp = new App(0);
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "bar")[0] == "Added";
            isPassed = !isPassed ? isPassed : mockApp.Add("baz", "bang")[0] == "Added";
            var result = mockApp.Keys();

            isPassed = !isPassed ? isPassed : result[0] == "foo";
            isPassed = !isPassed ? isPassed : result[1] == "baz";
            
            Console.WriteLine($"TEST: TestAppKeys Passed: {isPassed}");
        }

        private void TestAppMemberExists()
        {
            var isPassed = true;
            
            var mockApp = new App(0);
            isPassed = !isPassed ? isPassed : mockApp.MemberExists("foo", "bar")[0] == "false";
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "bar")[0] == "Added";
            isPassed = !isPassed ? isPassed : mockApp.MemberExists("foo", "bar")[0] == "true";
            isPassed = !isPassed ? isPassed : mockApp.MemberExists("foo", "baz")[0] == "false";
            
            Console.WriteLine($"TEST: TestAppMemberExists Passed: {isPassed}");
        }

        private void TestAppMemberExistsBool()
        {
            var isPassed = true;
            
            var mockApp = new App(0);
            isPassed = !isPassed ? isPassed : mockApp.MemberExistsBool("foo", "bar") == false;
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "bar")[0] == "Added";
            isPassed = !isPassed ? isPassed : mockApp.MemberExistsBool("foo", "bar") == true;
            isPassed = !isPassed ? isPassed : mockApp.MemberExistsBool("foo", "baz") == false;
            
            Console.WriteLine($"TEST: TestAppMemberExistsBool Passed: {isPassed}");
        }

        private void TestAppMembers()
        {
            var isPassed = true;
            
            var mockApp = new App(0);
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "bar")[0] == "Added";
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "baz")[0] == "Added";
            var result = mockApp.Members("foo");

            isPassed = !isPassed ? isPassed : result[0] == "bar";
            isPassed = !isPassed ? isPassed : result[1] == "baz";
            isPassed = !isPassed ? isPassed : mockApp.Members("bad")[0] == "ERROR, key does not exist.";
            
            Console.WriteLine($"TEST: TestAppMembers Passed: {isPassed}");
        }

        private void TestAppRemove()
        {
            var isPassed = true;
            
            var mockApp = new App(0);
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "bar")[0] == "Added";
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "baz")[0] == "Added";
            isPassed = !isPassed ? isPassed : mockApp.Remove("foo", "bar")[0] == "Removed";
            isPassed = !isPassed ? isPassed : mockApp.Remove("foo", "bar")[0] == "ERROR, member does not exist";
            isPassed = !isPassed ? isPassed : mockApp.Keys()[0] == "foo";
            isPassed = !isPassed ? isPassed : mockApp.Remove("foo", "baz")[0] == "Removed";
            isPassed = !isPassed ? isPassed : mockApp.Keys().Count() == 0;
            isPassed = !isPassed ? isPassed : mockApp.Remove("boom", "pow")[0] == "ERROR, key does not exist.";

            Console.WriteLine($"TEST: TestAppRemove Passed: {isPassed}");
        }

        private void TestAppRemoveAll()
        {
            var isPassed = true;
            
            var mockApp = new App(0);
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "bar")[0] == "Added";
            isPassed = !isPassed ? isPassed : mockApp.Add("foo", "baz")[0] == "Added";
            isPassed = !isPassed ? isPassed : mockApp.Keys()[0] == "foo";
            isPassed = !isPassed ? isPassed : mockApp.RemoveAll("foo")[0] == "Removed";
            isPassed = !isPassed ? isPassed : mockApp.Keys().Count() == 0;
            isPassed = !isPassed ? isPassed : mockApp.RemoveAll("foo")[0] == "ERROR, key does not exist.";
            
            Console.WriteLine($"TEST: TestAppRemoveAll Passed: {isPassed}");
        }
    }
}
