using System;
using System.Collections.Generic;
using BusinessObjects.Developer;

namespace BusinessObjects.Exchange
{
    /// <summary>
    /// ������ ��� �������
    /// </summary>
    [Serializable]
    public class Row
    {
        /// <summary>
        /// �������, � ������� ��������� ������ ������
        /// </summary>
        public DbObject Table;

        /// <summary>
        /// ������ ������
        /// </summary>
        public List<int> Id;
    }
}