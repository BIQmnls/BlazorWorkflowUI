using System.Text.Json;
using BlazorWorkflowUI.Models;
using System.Linq;

namespace BlazorWorkflowUI.Services;

public class WorkflowService : IWorkflowService
{
    private readonly Dictionary<string, WorkflowModel> _store = new();

    public string GenerateJson(WorkflowModel workflow)
    {
        var activities = new List<object>();
        var connections = new List<object>();

        for (int i = 0; i < workflow.Steps.Count; i++)
        {
            var step = workflow.Steps[i];
            activities.Add(new
            {
                id = step.Id,
                type = step.ActivityType,
                parameters = step.Parameters,
                files = step.Attachments.Select(a => new { name = a.FileName, contentType = a.ContentType, data = Convert.ToBase64String(a.Data) })
            });

            var nextId = step.NextStepId ?? workflow.Steps.ElementAtOrDefault(i + 1)?.Id;
            if (nextId != null)
            {
                connections.Add(new { source = step.Id, target = nextId });
            }

            if (step.ElseNextStepId != null)
            {
                connections.Add(new { source = step.Id, target = step.ElseNextStepId });
            }
        }

        var data = new
        {
            name = workflow.Name,
            trigger = new { type = workflow.Trigger.ActivityType, condition = workflow.Trigger.Condition == "NoCondition" ? null : workflow.Trigger.Condition },
            activities,
            connections
        };
        return JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
    }

    public void Save(WorkflowModel workflow)
    {
        _store[workflow.Name] = Clone(workflow);
    }

    public WorkflowModel? Load(string name)
    {
        return _store.TryGetValue(name, out var workflow) ? Clone(workflow) : null;
    }

    private static WorkflowModel Clone(WorkflowModel workflow)
    {
        var json = JsonSerializer.Serialize(workflow);
        return JsonSerializer.Deserialize<WorkflowModel>(json) ?? new WorkflowModel();
    }
}
