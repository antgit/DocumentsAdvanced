namespace BusinessObjects
{
    /// <summary>
    /// ��������� ���������� ������� �������� ��� ��������� ���� � ������
    /// </summary>
    /// <remarks>
    /// ���������� ������� �������� � �������������� �������������� ������� �������
    /// ����� � ������� �������� ���� � ������. 
    /// <para>��� ��������� ������ ������������ ������� �������� �������� �� ���������
    /// �����: ����� ������ + ��� ���� + ��� ���������, �.�. ��� ������� ����������
    /// �������������� <b>&quot;People&quot; </b>(������� &quot;Contractor.People&quot;)
    /// ������������ �������� ���������:</para>
    /// <para> </para>
    /// <list type="table">
    /// <listheader>
    /// <term>��� ���������</term>
    /// <description>������ ���</description></listheader>
    /// <item>
    /// <term>�������� ������</term>
    /// <description>Contractor.PeopleLoad</description></item>
    /// <item>
    /// <term>���������� ������</term>
    /// <description>Contractor.PeopleUpdate</description></item>
    /// <item>
    /// <term>�������� ������</term>
    /// <description>Contractor.PeopleDelete</description></item>
    /// <item>
    /// <term>��������</term>
    /// <description>Contractor.PeopleCreate</description></item></list>* ��������
    /// ��������� &quot;������ ���&quot; - ��� ���� �������� �������� ���������, � ��
    /// ������ ��� �������� ��������� � ���� ������. 
    /// <para> </para>
    /// </remarks>
    public interface IRelationSingle
    {
        string Schema { get; }
    }
}