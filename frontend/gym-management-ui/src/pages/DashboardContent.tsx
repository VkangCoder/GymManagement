import { Card, Col, Row, Statistic, Typography } from 'antd';

const { Title, Paragraph } = Typography;

export default function DashboardContent() {
  return (
    <div>
      <Title level={3}>Tổng quan</Title>
      <Paragraph type="secondary">
        Vùng Content này cuộn độc lập — Header và Sidebar luôn giữ nguyên vị trí.
      </Paragraph>

      <Row gutter={[16, 16]}>
        <Col xs={24} sm={12} lg={6}>
          <Card>
            <Statistic title="Hội viên" value={128} />
          </Card>
        </Col>
        <Col xs={24} sm={12} lg={6}>
          <Card>
            <Statistic title="Huấn luyện viên" value={12} />
          </Card>
        </Col>
        <Col xs={24} sm={12} lg={6}>
          <Card>
            <Statistic title="Thiết bị" value={54} />
          </Card>
        </Col>
        <Col xs={24} sm={12} lg={6}>
          <Card>
            <Statistic title="Gói tập đang hoạt động" value={342} />
          </Card>
        </Col>
      </Row>

      <Card style={{ marginTop: 24 }} title="Nội dung dài để kiểm tra scroll">
        {Array.from({ length: 30 }, (_, i) => (
          <Paragraph key={i}>
            Dòng nội dung mẫu số {i + 1} — thử cuộn Content để kiểm tra Header/Sidebar cố định.
          </Paragraph>
        ))}
      </Card>
    </div>
  );
}
