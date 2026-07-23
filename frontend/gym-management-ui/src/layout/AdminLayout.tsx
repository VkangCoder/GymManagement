import { Layout } from 'antd';
import { useState, type ReactNode } from 'react';
import SidebarTreeMenu from '../features/menu-tree/SidebarTreeMenu';
import './AdminLayout.css';
import AppHeader from './AppHeader';

const { Sider, Content } = Layout;

interface AdminLayoutProps {
  children: ReactNode;
}

export default function AdminLayout({ children }: AdminLayoutProps) {
  const [collapsed, setCollapsed] = useState(false);

  return (
    <Layout className="admin-shell">
      <AppHeader collapsed={collapsed} onToggleCollapsed={() => setCollapsed((c) => !c)} />
      <Layout className="admin-shell__body">
        <Sider
          className="admin-shell__sider"
          collapsible
          collapsed={collapsed}
          onCollapse={setCollapsed}
          trigger={null}
          width={260}
          collapsedWidth={64}
        >
          <SidebarTreeMenu collapsed={collapsed} onRequestExpand={() => setCollapsed(false)} />
        </Sider>
        <Content className="admin-shell__content">{children}</Content>
      </Layout>
    </Layout>
  );
}
