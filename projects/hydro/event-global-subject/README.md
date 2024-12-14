# Global event handling subject

This sample demonstrates the functionality of event subject. It allows components that subscribe to a specific message to filter which matching message they will receive. 

In this case, the `Message` component subscribes to `MessageChangedEvent`. The two instances of the component uses "left" and "right" event subject respectively. You will see how each instance react to the event they are subscribed to.   
