using System.Collections.Generic;
using System.Threading.Tasks;
using Storyteller.Core.Grammars;
using Storyteller.Core.Messages;
using Storyteller.Core.Model.Persistence;
using Storyteller.Core.Remotes.Messaging;
using Storyteller.Core.Results;

namespace Storyteller.Core.Engine.UserInterface
{
    // TODO -- do *something* to take the publishing off of the main thread
    public class UserInterfaceObserver : IUserInterfaceObserver
    {
        private void sendToClient(object o)
        {
            var passthrough = new PassthroughMessage(o);
            EventAggregator.SendMessage(passthrough);
        }

        public void SpecExecutionFinished(SpecificationPlan plan, ISpecContext context)
        {
            sendToClient(new SpecExecutionCompleted(plan.Specification.Id, context.Counts, 0));
        }

        public void Handle<T>(T message) where T : IResultMessage
        {
            sendToClient(message);
        }

        public void SpecQueued(SpecNode node)
        {
            sendToClient(new SpecQueued(node.id));
        }

        public void SendProgress(SpecProgress progress)
        {
            sendToClient(progress);
        }

        public void SpecStarted(SpecificationPlan plan)
        {
            sendToClient(new SpecRunning(plan.Specification.Id));
        }
    }
}