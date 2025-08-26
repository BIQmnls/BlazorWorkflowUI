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
                delay = step.ActivityType == "WaitForDocuments" ? step.DelaySeconds : null,
                condition = step.Condition == "NoCondition" ? null : step.Condition,
                text = step.Text,
                files = step.Attachments.Select(a => new { name = a.FileName, contentType = a.ContentType, data = Convert.ToBase64String(a.Data) })
            });

            var nextId = step.NextStepId ?? workflow.Steps.ElementAtOrDefault(i + 1)?.Id;
            if (nextId != null)
            {
                connections.Add(new { source = step.Id, target = nextId });
            }

            if (step.ElseActivityType != null)
            {
                var elseId = $"{step.Id}-else";
                activities.Add(new
                {
                    id = elseId,
                    type = step.ElseActivityType,
                    delay = step.ElseActivityType == "WaitForDocuments" ? step.ElseDelaySeconds : null,
                    text = step.ElseText,
                    files = step.ElseAttachments.Select(a => new { name = a.FileName, contentType = a.ContentType, data = Convert.ToBase64String(a.Data) })
                });

                var elseNextId = step.ElseNextStepId ?? workflow.Steps.ElementAtOrDefault(i + 1)?.Id;
                if (elseNextId != null)
                {
                    connections.Add(new { source = elseId, target = elseNextId });
                }
            }
        }

        var data = new
        {
            name = workflow.Name,
            trigger = workflow.Trigger.ActivityType,
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
