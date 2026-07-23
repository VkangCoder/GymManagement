import { MenuFoldOutlined, MenuUnfoldOutlined, UserOutlined } from '@ant-design/icons';
import { Avatar, Button, Layout, Space, Typography } from 'antd';

const { Header } = Layout;
const { Title } = Typography;

interface AppHeaderProps {
  collapsed: boolean;
  onToggleCollapsed: () => void;
}

export default function AppHeader({ collapsed, onToggleCollapsed }: AppHeaderProps) {
  return (
    <Header className="admin-shell__header">
      <Space size="middle" align="center">
        <Button
          type="text"
          icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
          onClick={onToggleCollapsed}
          aria-label={collapsed ? 'Mở rộng sidebar' : 'Thu gọn sidebar'}
        />
        <Title level={4} style={{ margin: 0 }}>
          Gym Management
        </Title>
      </Space>
      <Space size="middle" align="center">
        <Avatar icon={<UserOutlined />} />
        <span>Admin</span>
      </Space>
    </Header>
  );
}
