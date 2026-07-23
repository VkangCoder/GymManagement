import { DeleteOutlined, EditOutlined, PlusOutlined, ReloadOutlined } from '@ant-design/icons';
import { Dropdown, type MenuProps } from 'antd';
import type { ContextMenuAction, MenuTreeNode } from './types';

const contextMenuItems: MenuProps['items'] = [
  { key: 'add', icon: <PlusOutlined />, label: 'Thêm mới' },
  { key: 'edit', icon: <EditOutlined />, label: 'Sửa' },
  { key: 'delete', icon: <DeleteOutlined />, label: 'Xóa', danger: true },
  { type: 'divider' },
  { key: 'refresh', icon: <ReloadOutlined />, label: 'Làm mới' },
];

interface TreeContextMenuProps {
  x: number;
  y: number;
  node: MenuTreeNode;
  onClose: () => void;
  onAction: (action: ContextMenuAction, node: MenuTreeNode) => void;
}

export default function TreeContextMenu({ x, y, node, onClose, onAction }: TreeContextMenuProps) {
  return (
    <Dropdown
      open
      trigger={[]}
      placement="bottomLeft"
      onOpenChange={(open) => {
        if (!open) onClose();
      }}
      menu={{
        items: contextMenuItems,
        onClick: ({ key }) => {
          onAction(key as ContextMenuAction, node);
          onClose();
        },
      }}
    >
      {/* 1x1 anchor positioned at the cursor — Dropdown renders its popup relative to this. */}
      <div style={{ position: 'fixed', left: x, top: y, width: 1, height: 1 }} />
    </Dropdown>
  );
}
