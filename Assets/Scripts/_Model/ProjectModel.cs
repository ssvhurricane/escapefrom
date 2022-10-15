using System.Collections.Generic;

namespace Model
{
    public class ProjectModel 
    {
        public List<IModel> _projectModels { get; private set; }

        public List<object> _projectRules { get; private set; }

        public List<object> _projectFlows { get; private set; }

        public List<object> _projectWorlStates { get; private set; }

        public void AddModel(IModel model) 
        {
            // TODO:
        }
        public void AddModelById(string id)
        {
            // TODO:
        }

        public void RemoveModel(IModel model) 
        {
            // TODO:
        }
        public void RemoveModelById(string id)
        {
            // TODO:
        }

        public IModel GetModel(IModel model)
        {
            // TODO:
            return default(IModel);
        }

        public IModel GetModelById(string id)
        {
            // TODO:
            return default(IModel);
        }

        public void ClearAll() 
        {
            // TODO:
        }

        public void SaveCurrentModel() 
        { 
            // TODO: 
        }

        public object LoadCurrentModel()
        { 
            // TODO: 
            return default(object);
        }

        private object GetRules()
        {   
            // TODO: 
            return default(object);
        }
        private object GetFlows()
        {
            // TODO:
            return default(object);
        }

        private object GetWorldState() 
        { 
            // TODO:
            return default(object);
        }
    }
}
