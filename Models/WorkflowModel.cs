namespace BlazorWorkflowUI.Models;

public class WorkflowModel
{
    public string Name { get; set; } = "New Workflow";
    public TriggerModel Trigger { get; set; } = new();
    public List<StepModel> Steps { get; set; } = new();
}

public class TriggerModel
{
    public string ActivityType { get; set; } = "PolicyCreated";
}

public class StepModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ActivityType { get; set; } = "SendPolicyDocument";
    public int? DelaySeconds { get; set; }
    public string Condition { get; set; } = "NoCondition";
    public string? ElseActivityType { get; set; }
    public int? ElseDelaySeconds { get; set; }
    public string? Text { get; set; }
    public List<StepAttachmentModel> Attachments { get; set; } = new();
    public string? ElseText { get; set; }
    public List<StepAttachmentModel> ElseAttachments { get; set; } = new();
}

public class StepAttachmentModel
{
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public byte[] Data { get; set; } = Array.Empty<byte>();
}
