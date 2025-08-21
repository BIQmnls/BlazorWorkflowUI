using System.Text.Json;
using BlazorWorkflowUI.Models;

namespace BlazorWorkflowUI.Services;

public class WorkflowService : IWorkflowService
{
    public string GenerateJson(WorkflowModel workflow)
    {
        var activities = new List<object>();
        var connections = new List<object>();

        string? previousId = null;

        foreach (var step in workflow.Steps)
        {
            activities.Add(new
            {
                id = step.Id,
                type = step.ActivityType,
                delay = step.ActivityType == "WaitForDocuments" ? step.DelaySeconds : null,
                condition = step.Condition == "NoCondition" ? null : step.Condition
            });

            if (previousId != null)
            {
                connections.Add(new { source = previousId, target = step.Id });
            }

            if (step.ElseActivityType != null)
            {
                var elseId = $"{step.Id}-else";
                activities.Add(new
                {
                    id = elseId,
                    type = step.ElseActivityType,
                    delay = step.ElseActivityType == "WaitForDocuments" ? step.ElseDelaySeconds : null
                });

                if (previousId != null)
                {
                    connections.Add(new { source = previousId, target = elseId });
                }
            }

            previousId = step.Id;
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
}
