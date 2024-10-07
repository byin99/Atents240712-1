using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenTempSlot : InvenSlot
{
    /// <summary>
    /// 임시 슬롯용 인덱스(안쓰는 인덱스 숫자)
    /// </summary>
    const uint TempSlotIndex = 99999999;

    /// <summary>
    /// 드래그를 시작한 슬롯의 인덱스(null이면 드래그가 시작 안되었음)
    /// </summary>
    public uint? FromIndex
    {
        //get => FromIndex;
        //set => FromIndex = value;
        get;    // 위와 똑같음(FromIndex이름의 변수가 있는 것처럼 처리)
        set;
    }

    public InvenTempSlot() : base(TempSlotIndex)
    {
        FromIndex = null;
    }

    public override void ClearSlotItem()
    {
        base.ClearSlotItem();
        FromIndex = null;
    }

}
