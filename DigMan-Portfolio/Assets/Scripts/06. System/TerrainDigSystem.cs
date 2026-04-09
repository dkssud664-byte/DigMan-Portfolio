using UnityEngine;

public class TerrainDigSystem : MonoBehaviour
{
    [SerializeField] private float minHeight = 0f;   // 0 → 바닥
    [SerializeField] private float maxHeight = 1f;   // 1 → 최고 높이

    public void Dig(
       Terrain terrain,
       Vector3 hitPoint,
       float brushSize,
       float opacity)
    {
        ModifyHeight(terrain, hitPoint, brushSize, opacity, -1f);
    }

    public void Build(
        Terrain terrain,
        Vector3 hitPoint,
        float brushSize,
        float opacity)
    {
        ModifyHeight(terrain, hitPoint, brushSize, opacity, +1f);
    }

    public void ModifyHeight(Terrain terrain, Vector3 hitPoint, float brushSize,
        float opacity, float direction)
    {
        TerrainData data = terrain.terrainData;

        // 월드 -> Terrain 로컬 좌표
        Vector3 localPos = hitPoint - terrain.transform.position;

        //높이맵 배열 크기
        int res = data.heightmapResolution;

        //클릭 위치를 높이맵 인덱스 변환
        int centerX = Mathf.RoundToInt((localPos.x / data.size.x) * res);
        int centerZ = Mathf.RoundToInt((localPos.z / data.size.z) * res);

        //브러쉬 크기를 높이맵 기준으로 변환
        int brushRadius = Mathf.RoundToInt(
            (brushSize / data.size.x) * res);

        //브러쉬 시작 좌표 계산
        int startX = Mathf.Clamp(centerX - brushRadius, 0, res - 1);
        int startZ = Mathf.Clamp(centerZ - brushRadius, 0, res - 1);

        //수정 영역 크기 계산
        int width = Mathf.Clamp(brushRadius * 2, 1, res - startX);
        int height = Mathf.Clamp(brushRadius * 2, 1, res - startZ);

        //높이 데이터 가져오기 0은 최저 1은 최고
        float[,] heights = data.GetHeights(startX, startZ, width, height);

        //브러쉬 적용 루프
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                //브러쉬 중심과 거리 계산
                float distance = Vector2.Distance(
                    new Vector2(x, z),
                    new Vector2(brushRadius, brushRadius));

                //원 밖 예외처리
                if (distance > brushRadius)
                    continue;

                //중심은 강하게 가장자리는 약하게
                float falloff = 1f - (distance / brushRadius);
                //높이 변화량 계산
                float delta = opacity * falloff * direction;

                //높이 수정
                heights[z, x] += delta;

                //최소/최대 높이 제한
                heights[z, x] = Mathf.Clamp(
                    heights[z, x],
                    minHeight,
                    maxHeight);
            }
        }

        //Terrain에 적용
        data.SetHeights(startX, startZ, heights);
    }
}
