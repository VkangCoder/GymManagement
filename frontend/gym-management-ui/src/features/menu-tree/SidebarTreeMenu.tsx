import { Empty, Tooltip, Tree, type TreeDataNode, type TreeProps } from 'antd';
import { useState } from 'react';
import './SidebarTreeMenu.css';
import { resolveMenuIcon } from './iconMap';
import MenuFormModal from './MenuFormModal';
import TreeContextMenu from './TreeContextMenu';
import type { ContextMenuAction, MenuFormModalState, MenuTreeNode } from './types';
import { findNode, useMenuTree } from './useMenuTree';

function toDataNodes(nodes: MenuTreeNode[]): TreeDataNode[] {
  return nodes.map((node) => ({
    key: node.key,
    title: node.title,
    icon: () => resolveMenuIcon(node.icon),
    children: node.children ? toDataNodes(node.children) : undefined,
  }));
}

interface ContextMenuState {
  x: number;
  y: number;
  node: MenuTreeNode;
}

interface SidebarTreeMenuProps {
  collapsed: boolean;
  onRequestExpand: () => void;
}

export default function SidebarTreeMenu({ collapsed, onRequestExpand }: SidebarTreeMenuProps) {
  const { treeData, loading, addNode, editNode, deleteNode, refresh } = useMenuTree();
  const [contextMenu, setContextMenu] = useState<ContextMenuState | null>(null);
  const [modalState, setModalState] = useState<MenuFormModalState>(null);

  const handleRightClick: NonNullable<TreeProps['onRightClick']> = ({ event, node }) => {
    event.preventDefault();
    const target = findNode(treeData, String(node.key));
    if (target) setContextMenu({ x: event.clientX, y: event.clientY, node: target });
  };

  const handleAction = (action: ContextMenuAction, node: MenuTreeNode) => {
    switch (action) {
      case 'add':
        setModalState({ mode: 'add', parentKey: node.key });
        break;
      case 'edit':
        setModalState({ mode: 'edit', node });
        break;
      case 'delete':
        deleteNode(node.key);
        break;
      case 'refresh':
        void refresh();
        break;
    }
  };

  const handleModalSubmit = (title: string) => {
    if (modalState?.mode === 'add') addNode(modalState.parentKey, title);
    if (modalState?.mode === 'edit') editNode(modalState.node.key, title);
    setModalState(null);
  };

  if (collapsed) {
    return (
      <div className="sidebar-rail">
        {treeData.map((node) => (
          <Tooltip key={node.key} title={node.title} placement="right">
            <button type="button" className="sidebar-rail__item" onClick={onRequestExpand}>
              {resolveMenuIcon(node.icon)}
            </button>
          </Tooltip>
        ))}
      </div>
    );
  }

  return (
    <div className="sidebar-tree-menu">
      {treeData.length === 0 ? (
        <Empty description="Không có menu" className="sidebar-tree-menu__empty" />
      ) : (
        <Tree
          treeData={toDataNodes(treeData)}
          defaultExpandAll
          showIcon
          blockNode
          selectable
          className="sidebar-tree-menu__tree"
          onRightClick={handleRightClick}
        />
      )}

      {loading && <div className="sidebar-tree-menu__loading">Đang tải…</div>}

      {contextMenu && (
        <TreeContextMenu
          x={contextMenu.x}
          y={contextMenu.y}
          node={contextMenu.node}
          onClose={() => setContextMenu(null)}
          onAction={handleAction}
        />
      )}

      <MenuFormModal state={modalState} onClose={() => setModalState(null)} onSubmit={handleModalSubmit} />
    </div>
  );
}
