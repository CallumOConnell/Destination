namespace Destination
{
    public class QuestPath
    {
        public QuestEvent startEvent;
        public QuestEvent endEvent;

        public QuestPath(QuestEvent _from, QuestEvent _to)
        {
            startEvent = _from;
            endEvent = _to;
        }
    }
}